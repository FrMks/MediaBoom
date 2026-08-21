using CSharpFunctionalExtensions;
using MediaBoomFileService.Domain.Enums;
using Shared;

namespace MediaBoomFileService.Domain.MediaProcessing;

/// <summary>
/// Represents one step of an processing pipeline.
/// </summary>
public sealed class ProcessingStep
{
    /// <summary>
    /// Creates a processing step with the specified type, order and progress weight.
    /// </summary>
    /// <param name="stepType">The type of processing step.</param>
    /// <param name="order">The execution order of the step.</param>
    /// <param name="weight">The step's contribution to total progress.</param>
    public ProcessingStep(StepType stepType, int order, int weight)
    {
        Id = Guid.NewGuid();
        StepType = stepType;
        Order = order;
        Weight = weight;
        Status = StepStatus.PENDING;
    }

    // EF Core
    private ProcessingStep()
    {
    }

    /// <summary>
    /// Gets the unique identifier of the processing step.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the type of the processing step.
    /// </summary>
    public StepType StepType { get; private set; }

    /// <summary>
    /// Gets the current status of the processing step.
    /// </summary>
    public StepStatus Status { get; private set; }

    /// <summary>
    /// The order in which the step is executed.
    /// </summary>
    public int Order { get; private set; }

    /// <summary>
    /// The step's contribution to the total process progress.
    /// </summary>
    public int Weight { get; private set; }

    /// <summary>
    /// Gets the result data produced by the step, if any.
    /// </summary>
    public string? ResultData { get; private set; }

    /// <summary>
    /// Gets the error message when the step fails.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Gets the time when the step started.
    /// </summary>
    public DateTime? StartedAt { get; private set; }

    /// <summary>
    /// Gets the time when the step completed or failed.
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    internal UnitResult<Error> Start()
    {
        if (Status != StepStatus.PENDING)
        {
            return Error.Validation(
                "step.invalid.status",
                $"Can only start step from PENDING status, current: {Status}");
        }

        Status = StepStatus.IN_PROGRESS;
        StartedAt = DateTime.UtcNow;

        return UnitResult.Success<Error>();
    }

    internal UnitResult<Error> Complete(string? resultData = null)
    {
        if (Status != StepStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "step.invalid.status",
                $"Can only complete step from IN_PROGRESS status, current: {Status}");
        }

        Status = StepStatus.COMPLETED;
        ResultData = resultData;
        CompletedAt = DateTime.UtcNow;

        return UnitResult.Success<Error>();
    }

    internal UnitResult<Error> Fail(string errorMessage)
    {
        if (Status != StepStatus.IN_PROGRESS)
        {
            return Error.Validation(
                "step.invalid.status",
                $"Can only fail step from IN_PROGRESS status, current: {Status}");
        }

        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            return Error.Validation(
                "step.error.required",
                "Error message is required");
        }

        Status = StepStatus.FAILED;
        ErrorMessage = errorMessage.Trim();
        CompletedAt = DateTime.UtcNow;

        return UnitResult.Success<Error>();
    }

    internal void Reset()
    {
        Status = StepStatus.PENDING;
        ResultData = null;
        ErrorMessage = null;
        StartedAt = null;
        CompletedAt = null;
    }
}
