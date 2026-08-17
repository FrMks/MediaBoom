using CSharpFunctionalExtensions;
using MediaBoomFileService.Domain.Enums;
using Shared;

namespace MediaBoomFileService.Domain.MediaProcessing;

/// <summary>
/// Represents the processing pipeline for an audio asset.
/// </summary>
public sealed class AudioProcess
{
    private readonly List<ProcessingStep> _steps = [];

    private static readonly List<(StepType StepType, int Weight)> StepDefinitions =
    [
        (StepType.INITIALIZE, 0),
        (StepType.DOWNLOAD_SOURCE, 0),
        (StepType.EXTRACT_METADATA, 10),
        (StepType.GENERATE_HLS, 65),
        (StepType.UPLOAD_HLS, 20),
        (StepType.CLEANUP, 5),
    ];

    /// <summary>
    /// Creates a new audio processing pipeline for the specified asset.
    /// </summary>
    /// <param name="audioAssetId">The identifier of the audio asset to process.</param>
    public AudioProcess(Guid audioAssetId)
    {
        Id = Guid.NewGuid();
        AudioAssetId = audioAssetId;
        Status = ProcessingStatus.IN_PROGRESS;
        StartedAt = DateTime.UtcNow;

        InitializeSteps();
    }

    // EF Core
    private AudioProcess()
    {
    }

    /// <summary>
    /// Gets the unique identifier of the processing process.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the identifier of the audio asset being processed.
    /// </summary>
    public Guid AudioAssetId { get; private set; }

    /// <summary>
    /// Gets the current status of the processing process.
    /// </summary>
    public ProcessingStatus Status { get; private set; }

    /// <summary>
    /// Gets the completed progress percentage of the processing process.
    /// </summary>
    public int ProgressPercentage { get; private set; }

    /// <summary>
    /// Gets the error message when processing fails.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the process failed with an error that must not be retried.
    /// </summary>
    public bool IsCriticalError { get; private set; }

    /// <summary>
    /// Gets the number of retries already scheduled for the process.
    /// </summary>
    public int RetryCount { get; private set; }

    /// <summary>
    /// Gets the maximum number of retries allowed for the process.
    /// </summary>
    public int MaxRetries { get; private set; } = 3;

    /// <summary>
    /// Gets the time when the next retry may be started.
    /// </summary>
    public DateTime? NextRetryAt { get; private set; }

    /// <summary>
    /// Gets the time when processing started.
    /// </summary>
    public DateTime StartedAt { get; private set; }

    /// <summary>
    /// Gets the time when processing completed or failed.
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    /// Gets all steps in execution order.
    /// </summary>
    public IReadOnlyList<ProcessingStep> Steps => _steps;

    /// <summary>
    /// Gets the step currently in progress, if any.
    /// </summary>
    public ProcessingStep? CurrentStep =>
        _steps.FirstOrDefault(step => step.Status == StepStatus.IN_PROGRESS);

    /// <summary>
    /// Starts the next pending step or completes the process when all steps are done.
    /// </summary>
    public Result<ProcessingStep?, Error> ProcessNextStep()
    {
        if (Status != ProcessingStatus.IN_PROGRESS)
        {
            return Error.Failure(
                "processing.invalid.status",
                $"Cannot process step when status is {Status}");
        }

        ProcessingStep? currentStep = CurrentStep;
        if (currentStep is not null)
            return currentStep;

        ProcessingStep? nextStep = _steps
            .OrderBy(step => step.Order)
            .FirstOrDefault(step => step.Status == StepStatus.PENDING);

        if (nextStep is null)
        {
            UnitResult<Error> completeResult = Complete();
            if (completeResult.IsFailure)
                return completeResult.Error;

            return Result.Success<ProcessingStep?, Error>(null);
        }

        UnitResult<Error> startResult = nextStep.Start();
        if (startResult.IsFailure)
            return startResult.Error;

        return nextStep;
    }

    /// <summary>
    /// Completes the currently active processing step.
    /// </summary>
    /// <param name="resultData">Optional result data produced by the step.</param>
    public UnitResult<Error> CompleteCurrentStep(string? resultData = null)
    {
        if (Status != ProcessingStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "processing.invalid.status",
                $"Cannot complete step when status is {Status}");
        }

        ProcessingStep? currentStep = CurrentStep;
        if (currentStep is null)
            return Error.Validation("processing.no.active.step", "No active step to complete");

        UnitResult<Error> result = currentStep.Complete(resultData);
        if (result.IsFailure)
            return result.Error;

        ProgressPercentage = _steps
            .Where(step => step.Status == StepStatus.COMPLETED)
            .Sum(step => step.Weight);

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Fails the currently active processing step.
    /// </summary>
    /// <param name="errorMessage">The reason why the step failed.</param>
    public UnitResult<Error> FailCurrentStep(string errorMessage)
    {
        if (Status != ProcessingStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "processing.invalid.status",
                $"Cannot fail step when status is {Status}");
        }

        ProcessingStep? currentStep = CurrentStep;
        if (currentStep is null)
            return Error.Validation("processing.no.active.step", "No active step to fail");

        return currentStep.Fail(errorMessage);
    }

    /// <summary>
    /// Marks the whole processing pipeline as failed.
    /// </summary>
    /// <param name="errorMessage">The reason why processing failed.</param>
    /// <param name="isCritical">Whether the failure must not be retried.</param>
    public UnitResult<Error> Fail(string errorMessage, bool isCritical = false)
    {
        if (Status != ProcessingStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "processing.invalid.status",
                $"Cannot fail when status is {Status}");
        }

        if (string.IsNullOrWhiteSpace(errorMessage))
            return Error.Validation("processing.error.required", "Error message is required");

        if (CurrentStep is not null)
        {
            UnitResult<Error> stepResult = CurrentStep.Fail(errorMessage);
            if (stepResult.IsFailure)
                return stepResult.Error;
        }

        Status = ProcessingStatus.FAILED;
        ErrorMessage = errorMessage.Trim();
        CompletedAt = DateTime.UtcNow;
        IsCriticalError = isCritical;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Indicates whether the failed process can be retried.
    /// </summary>
    public bool CanRetry() => RetryCount < MaxRetries && !IsCriticalError;

    /// <summary>
    /// Resets a failed process and all of its steps for another attempt.
    /// </summary>
    public UnitResult<Error> Reset()
    {
        if (Status != ProcessingStatus.FAILED)
        {
            return Error.Validation(
                "processing.invalid.status",
                "Can only reset from FAILED status");
        }

        Status = ProcessingStatus.IN_PROGRESS;
        ProgressPercentage = 0;
        CompletedAt = null;
        ErrorMessage = null;
        IsCriticalError = false;

        foreach (ProcessingStep step in _steps)
            step.Reset();

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Schedules a retry for a failed process.
    /// </summary>
    /// <param name="nextRetryAt">The time when the retry should become eligible.</param>
    public UnitResult<Error> ScheduleRetry(DateTime nextRetryAt)
    {
        if (Status != ProcessingStatus.FAILED)
        {
            return Error.Validation(
                "processing.invalid.status",
                "Can only schedule a retry from FAILED status");
        }

        if (IsCriticalError)
            return Error.Validation("processing.retry.critical", "Cannot retry critical failure");

        if (RetryCount >= MaxRetries)
            return Error.Validation("processing.retry.exhausted", "Max retries exceeded");

        RetryCount++;
        NextRetryAt = nextRetryAt;

        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Completes the process only when every step has completed successfully.
    /// </summary>
    public UnitResult<Error> Complete()
    {
        if (Status != ProcessingStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "processing.invalid.status",
                $"Can only complete from IN_PROGRESS status, current status: {Status}");
        }

        if (_steps.Any(step => step.Status != StepStatus.COMPLETED))
        {
            return Error.Validation(
                "processing.incomplete.steps",
                "Cannot complete processing when not all steps are completed");
        }

        Status = ProcessingStatus.COMPLETED;
        CompletedAt = DateTime.UtcNow;
        ProgressPercentage = 100;

        return UnitResult.Success<Error>();
    }

    private void InitializeSteps()
    {
        int order = 1;
        foreach ((StepType stepType, int weight) in StepDefinitions)
        {
            _steps.Add(new ProcessingStep(stepType, order++, weight));
        }
    }
}
