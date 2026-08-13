using CSharpFunctionalExtensions;
using Shared;
using System.Text.RegularExpressions;

namespace MediaBoomService.Domain.Users.ValueObjects;

/// <summary>
/// Represents an username value object.
/// </summary>
public record Username
{
    private static readonly Regex ValidFormat = new(
        "^[A-Za-z](?:[A-Za-z]|[._-](?=[A-Za-z])){2,31}$",
        RegexOptions.CultureInvariant);

    private Username() { }

    private Username(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Get the username value.
    /// </summary>
    public string Value { get; }

    public static Result<Username, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation("username.required", "Username cannot be empty.");
        }

        if (value.Length < LengthConstants.LENGTH3 || value.Length > LengthConstants.LENGTH32)
        {
            return Error.Validation(
                "username.length.invalid",
                $"Username must contain from {LengthConstants.LENGTH3} to {LengthConstants.LENGTH32} characters.");
        }

        if (!ValidFormat.IsMatch(value))
        {
            return Error.Validation(
                "username.format.invalid",
                "Username may contain Latin letters and single '.', '-' or '_' separators only.");
        }

        return Result.Success<Username, Error>(new Username(value));
    }
}
