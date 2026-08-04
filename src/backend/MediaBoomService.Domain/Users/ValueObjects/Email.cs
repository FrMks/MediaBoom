using CSharpFunctionalExtensions;
using MediaBoomService.Domain;
using Shared;

namespace MediaBoomService.Domain.Users.ValueObjects;

/// <summary>
/// Represents an email address value object.
/// </summary>
public record Email
{
    // Ef core
    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the email address value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="Email"/> class with the specified value.
    /// </summary>
    /// <param name="value">The email address value.</param>
    /// <returns>The result of the creation operation.</returns>
    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(null, "Email cannot be empty.");
        }

        if (value.Length > LengthConstants.LENGTH254)
        {
            return Error.Validation(
                "email.length.is.invalid",
                $"Email cannot be longer than {LengthConstants.LENGTH254} characters.");
        }

        if (!IsValidEmail(value))
        {
            return Error.Validation(null, "Invalid email format.");
        }

        return Result.Success<Email, Error>(new Email(value));
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
