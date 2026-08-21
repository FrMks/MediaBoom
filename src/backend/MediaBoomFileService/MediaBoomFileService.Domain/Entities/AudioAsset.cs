using CSharpFunctionalExtensions;
using Shared;
using MediaBoomFileService.Domain.Enums;
using MediaBoomFileService.Domain.ValueObjects;

namespace MediaBoomFileService.Domain.Entities;

/// <summary>
/// Represents an audio asset and metadata produced while analyzing its source file.
/// </summary>
public class AudioAsset : MediaAsset
{
    private AudioAsset()
        : base() { }

    private AudioAsset(
        Guid id,
        AssetType assetType,
        MediaData mediaData,
        MediaStatus status,
        StorageKey? sourceObjectKey,
        string? failureReason,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime? readyAt)
        : base(
            id,
            assetType,
            mediaData,
            status,
            sourceObjectKey,
            failureReason,
            createdAt,
            updatedAt,
            readyAt)
    {
    }

    /// <summary>
    /// The object-storage bucket used for processed audio assets.
    /// </summary>
    public const string BUCKET = "audio";

    /// <summary>
    /// Duration of the source audio file in milliseconds.
    /// </summary>
    public long? DurationMs { get; private set; }

    /// <summary>
    /// Container format detected in the uploaded source file.
    /// </summary>
    public string? SourceContainer { get; private set; }

    /// <summary>
    /// Codec detected in the uploaded source file.
    /// </summary>
    public string? SourceCodec { get; private set; }

    /// <summary>
    /// Object-storage key of the generated HLS manifest.
    /// </summary>
    public StorageKey? HlsManifestObjectKey { get; private set; }

    /// <summary>
    /// Creates a new audio asset in the uploading state.
    /// </summary>
    /// <param name="mediaData">Original metadata of the uploaded audio file.</param>
    /// <param name="sourceObjectKey">Object-storage key of the temporary source file.</param>
    /// <returns>The created audio asset or a validation error.</returns>
    public static Result<AudioAsset, Error> Create(
        MediaData mediaData,
        StorageKey sourceObjectKey)
    {
        if (mediaData is null)
        {
            return Error.Validation(
                "audio.media-data.required",
                "Media data is required");
        }

        if (mediaData.ContentType.Category != MediaType.AUDIO)
        {
            return Error.Validation(
                "audio.invalid.content-type",
                "File content type must be audio");
        }

        if (sourceObjectKey is null || sourceObjectKey == StorageKey.None)
        {
            return Error.Validation(
                "audio.source-object-key.required",
                "Source object key is required");
        }

        DateTime now = DateTime.UtcNow;

        return new AudioAsset(
            Guid.NewGuid(),
            AssetType.AUDIO,
            mediaData,
            MediaStatus.UPLOADING,
            sourceObjectKey,
            null,
            now,
            now,
            null);
    }
}
