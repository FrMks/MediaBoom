namespace MediaBoomFileService.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of an audio processing process.
/// </summary>
public enum ProcessingStatus
{
    /// <summary>
    /// Processing is currently in progress.
    /// </summary>
    IN_PROGRESS,

    /// <summary>
    /// All processing steps completed successfully.
    /// </summary>
    COMPLETED,

    /// <summary>
    /// Processing stopped because of an error.
    /// </summary>
    FAILED,
}
