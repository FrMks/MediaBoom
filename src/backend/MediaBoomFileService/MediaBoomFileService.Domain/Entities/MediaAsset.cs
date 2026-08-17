using MediaBoomFileService.Domain.Enums;
using MediaBoomFileService.Domain.ValueObjects;

namespace MediaBoomFileService.Domain.Entities;

/// <summary>
/// Represents an uploaded audio file and its lifecycle in the file service.
/// </summary>
public abstract class MediaAsset
{
    /// <summary>
    /// The main Id for MediaAsset.
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Business type of the asset. Currently this service supports audio assets.
    /// </summary>
    public AssetType AssetType { get; protected set; }

    /// <summary>
    /// Original metadata supplied for the uploaded file.
    /// </summary>
    public MediaData MediaData { get; protected set; } = null!;

    /// <summary>
    /// Store current status of media (Uploading, Processing, Failed..).
    /// </summary>
    public MediaStatus Status { get; protected set; }

    /// <summary>
    /// The key for the temporary source file in MinIO.
    /// It can be cleared after successful audio processing.
    /// </summary>
    public string? SourceObjectKey { get; protected set; }

    /// <summary>
    /// Reason of last error processing.
    /// </summary>
    public string? FailureReason { get; protected set; } 

    /// <summary>
    /// Time when the asset was created.
    /// </summary>
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// Time when the asset was last changed.
    /// </summary>
    public DateTime UpdatedAt { get; protected set; }

    /// <summary>
    /// Time when the processed audio became available.
    /// </summary>
    public DateTime? ReadyAt { get; protected set; }
}
