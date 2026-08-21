using CSharpFunctionalExtensions;
using Shared;

namespace MediaBoomFileService.Domain.ValueObjects;

/// <summary>
/// Original metadata supplied for the uploaded file.
/// </summary>
public sealed record MediaData
{
    /// <summary>
    /// Original name of file, which uploaded user.
    /// </summary>
    public FileName FileName { get; init; } = null!;

    /// <summary>
    /// MIME-type, which set client (audio/mpeg, audio/ogg, audio/mp4...).
    /// Use for preliminary check.
    /// </summary>
    public ContentType ContentType { get; init; } = null!;

    /// <summary>
    /// Size of the source file in bytes.
    /// </summary>
    public long SizeBytes { get; init; }

    /// <summary>
    /// How many chunks we need to have for store media by chunks.
    /// </summary>
    public int ExpectedChunksCount { get; init; }

    private MediaData() { }

    private MediaData(FileName fileName, ContentType contentType, long sizeBytes, int expectedChunksCount)
    {
        FileName = fileName;
        ContentType = contentType;
        SizeBytes = sizeBytes;
        ExpectedChunksCount = expectedChunksCount;
    }

    /// <summary>
    /// Create method for MediaData Value Object.
    /// </summary>
    /// <param name="fileName">Filename of uploaded file.</param>
    /// <param name="contentType">Content type of uploaded file.</param>
    /// <param name="size">Size by bytes of uploaded file.</param>
    /// <param name="expectedChunksCount">How many chunks we need to have for store media by chunks.</param>
    /// <returns>Return success Value Object or Error if failure.</returns>
    public static Result<MediaData, Error> Create(
        FileName fileName,
        ContentType contentType,
        long size,
        int expectedChunksCount)
    {
        if (size <= 0)
            return Error.Validation(null, "Size should be greater 0");

        if (expectedChunksCount <= 0)
            return Error.Validation(null, "Expected chunks count should be greater 0");

        return new MediaData(fileName, contentType, size, expectedChunksCount);
    }
}
