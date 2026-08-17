using CSharpFunctionalExtensions;
using MediaBoomFileService.Domain.Enums;
using Shared;

namespace MediaBoomFileService.Domain.ValueObjects;

public sealed record ContentType
{
    /// <summary>
    /// MIME type/content type. For example: "audio/mpeg", "audio/ogg", "audio/mp4".
    /// </summary>
    public string Value { get; init; } = null!;

    /// <summary>
    /// MIME category of the file.
    /// </summary>
    public MediaType Category { get; init; }

    private ContentType() { }

    private ContentType(string value, MediaType category)
    {
        Value = value;
        Category = category;
    }

    /// <summary>
    /// Create method for ContentType value object with validation.
    /// </summary>
    /// <param name="contentType">Ful content type of file.</param>
    /// <returns>Return success Value object or Error.</returns>
    public static Result<ContentType, Error> Create(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return Error.Validation("value.is.null", nameof(contentType));

        MediaType category = contentType switch
        {
            _ when contentType.Contains("audio", StringComparison.InvariantCultureIgnoreCase) => MediaType.AUDIO,
            _ => MediaType.UNKNOWN,
        };

        return new ContentType(contentType, category);
    }
}
