using MediaBoomService.Domain.Users.ValueObjects;

namespace MediaBoomService.Domain.Users;

public sealed class User
{
    private User() { }

    private User(
        Guid id,
        Email email,
        string logoUrl,
        DateTime createdAtUtc,
        string passwordHash,
        string username)
    {
        Id = id;
        Email = email;
        LogoUrl = logoUrl;
        CreatedAtUtc = createdAtUtc;
        PasswordHash = passwordHash;
        Username = username;
    }

    public Guid Id { get; private set; }

    public Email Email { get; private set; }

    public string LogoUrl { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public string PasswordHash { get; private set; }

    public string Username { get; private set; }
}
