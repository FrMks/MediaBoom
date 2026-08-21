namespace MediaBoomFileService.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of a processing step.
/// </summary>
public enum StepStatus
{
    /// <summary>
    /// The step has not started yet.
    /// </summary>
    PENDING,

    /// <summary>
    /// The step is currently running.
    /// </summary>
    IN_PROGRESS,

    /// <summary>
    /// The step completed successfully.
    /// </summary>
    COMPLETED,

    /// <summary>
    /// The step failed.
    /// </summary>
    FAILED,
}
