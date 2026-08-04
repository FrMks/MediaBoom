using CSharpFunctionalExtensions;
using Shared;

namespace MediaBoomService.Domain.Users.ValueObjects;

public record Email
{
    private Email() { }

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(null, "Email cannot be empty.");
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
