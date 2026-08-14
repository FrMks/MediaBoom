using CSharpFunctionalExtensions;
using Shared;

namespace MediaBoomService.Domain.Compositions.ValueObjects;

public record ArtistName
{
    private ArtistName(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the normalized artist name.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates an artist name from the specified value.
    /// </summary>
    /// <param name="value">The artist name to validate and normalize.</param>
    /// <returns>
    /// A successful result containing the artist name, or a validation error when the value is invalid.
    /// </returns>
    public static Result<ArtistName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation("composition.artist-name.required", "Artist name cannot be empty.");
        }

        var trimmedValue = value.Trim();

        if (trimmedValue.Length > LengthConstants.LENGTH150)
        {
            return Error.Validation(
                "composition.artist-name.length.invalid",
                $"Artist name cannot be longer than {LengthConstants.LENGTH150} characters.");
        }

        return Result.Success<ArtistName, Error>(new ArtistName(trimmedValue));
    }
}
