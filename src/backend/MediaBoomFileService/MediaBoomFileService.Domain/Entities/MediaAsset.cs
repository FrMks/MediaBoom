using CSharpFunctionalExtensions;
using MediaBoomFileService.Domain.Enums;
using MediaBoomFileService.Domain.ValueObjects;
using Shared;

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
    public StorageKey? SourceObjectKey { get; protected set; }

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

    #region Constructor

    protected MediaAsset() { }

    protected MediaAsset(
        Guid id,
        AssetType assetType,
        MediaData mediaData,
        MediaStatus status,
        StorageKey? sourceObjectKey,
        string? failureReason,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime? readyAt)
    {
        Id = id;
        AssetType = assetType;
        MediaData = mediaData;
        Status = status;
        SourceObjectKey = sourceObjectKey;
        FailureReason = failureReason;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ReadyAt = readyAt;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Marks the asset as fully uploaded and ready for processing.
    /// </summary>
    public UnitResult<Error> MarkUploaded(DateTime timestamp)
    {
        return ChangeStatus(MediaStatus.UPLOADED, timestamp);
    }

    /// <summary>
    /// Marks the asset as being processed by the audio processing pipeline.
    /// </summary>
    public UnitResult<Error> MarkProcessing(DateTime timestamp)
    {
        return ChangeStatus(MediaStatus.PROCESSING, timestamp);
    }

    /// <summary>
    /// Marks the asset as ready for playback and records when processing completed.
    /// </summary>
    public UnitResult<Error> MarkReady(DateTime timestamp)
    {
        UnitResult<Error> result = ChangeStatus(MediaStatus.READY, timestamp);
        if (result.IsFailure)
            return result.Error;

        ReadyAt = timestamp;
        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Marks the asset as failed and stores the reason for the processing failure.
    /// </summary>
    public UnitResult<Error> MarkFailed(string failureReason, DateTime timestamp)
    {
        if (string.IsNullOrWhiteSpace(failureReason))
        {
            return Error.Validation(
                "media.failure-reason.required",
                "Failure reason is required");
        }

        UnitResult<Error> result = ChangeStatus(MediaStatus.FAILED, timestamp);
        if (result.IsFailure)
            return result.Error;

        FailureReason = failureReason.Trim();
        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Marks the asset as logically deleted.
    /// </summary>
    public UnitResult<Error> MarkDeleted(DateTime timestamp)
    {
        return ChangeStatus(MediaStatus.DELETED, timestamp);
    }

    /// <summary>
    /// Changes the asset status after validating the lifecycle transition.
    /// </summary>
    protected UnitResult<Error> ChangeStatus(MediaStatus target, DateTime timestamp)
    {
        if (Status == target)
            return UnitResult.Success<Error>();

        if (!CanChangeStatusTo(target))
        {
            return Error.Validation(
                "media.invalid.status-transition",
                $"Cannot change status from {Status} to {target}");
        }

        Status = target;
        UpdatedAt = timestamp;
        return UnitResult.Success<Error>();
    }

    /// <summary>
    /// Determines whether the asset can transition to the requested status.
    /// </summary>
    protected virtual bool CanChangeStatusTo(MediaStatus target)
    {
        return Status switch
        {
            MediaStatus.UPLOADING => target is MediaStatus.UPLOADED or MediaStatus.FAILED or MediaStatus.DELETED,
            MediaStatus.UPLOADED => target is MediaStatus.PROCESSING or MediaStatus.FAILED or MediaStatus.DELETED,
            MediaStatus.PROCESSING => target is MediaStatus.READY or MediaStatus.FAILED or MediaStatus.DELETED,
            MediaStatus.READY => target == MediaStatus.DELETED,
            MediaStatus.FAILED => target == MediaStatus.DELETED,
            MediaStatus.DELETED => false,
            _ => false,
        };
    }

    #endregion
}
