using CSharpFunctionalExtensions;
using Shared;

namespace MediaBoomService.Domain.Compositions.ValueObjects;

public record Title
{
    private Title(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the normalized composition title.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a composition title from the specified value.
    /// </summary>
    /// <param name="value">The title to validate and normalize.</param>
    /// <returns>
    /// A successful result containing the title, or a validation error when the value is invalid.
    /// </returns>
    public static Result<Title, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation("composition.title.required", "Composition title cannot be empty.");
        }

        var trimmedValue = value.Trim();

        if (trimmedValue.Length > LengthConstants.LENGTH200)
        {
            return Error.Validation(
                "composition.title.length.invalid",
                $"Composition title cannot be longer than {LengthConstants.LENGTH200} characters.");
        }

        return Result.Success<Title, Error>(new Title(trimmedValue));
    }
}
