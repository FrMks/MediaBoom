namespace MediaBoomFileService.Domain.Enums;

/// <summary>
/// Defines the steps of the audio processing pipeline.
/// </summary>
public enum StepType
{
    /// <summary>
    /// Initializes the processing context.
    /// </summary>
    INITIALIZE,

    /// <summary>
    /// Downloads or opens the source audio file for processing.
    /// </summary>
    DOWNLOAD_SOURCE,

    /// <summary>
    /// Extracts technical metadata from the source audio file.
    /// </summary>
    EXTRACT_METADATA,

    /// <summary>
    /// Generates the HLS playlist and audio segments.
    /// </summary>
    GENERATE_HLS,

    /// <summary>
    /// Uploads the generated HLS files to object storage.
    /// </summary>
    UPLOAD_HLS,

    /// <summary>
    /// Removes temporary processing artifacts.
    /// </summary>
    CLEANUP,
}
