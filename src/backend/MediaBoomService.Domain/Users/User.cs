using CSharpFunctionalExtensions;
using MediaBoomService.Domain.Users.ValueObjects;

namespace MediaBoomService.Domain.Users;

/// <summary>
/// Represents a user in the system.
/// </summary>
public sealed class User
{
    // Ef core
    private User() { }

    private User(
        UserId id,
        Email email,
        string logoUrl,
        string passwordHash,
        string username,
        DateTime createdAtUtc)
    {
        Id = id;
        Email = email;
        LogoUrl = logoUrl;
        PasswordHash = passwordHash;
        Username = username;
        CreatedAtUtc = createdAtUtc;
    }

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId Id { get; private set; } = null!;

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the URL of the user's logo.
    /// </summary>
    public string LogoUrl { get; private set; }

    /// <summary>
    /// Gets the hashed password of the user.
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets the UTC timestamp when the user was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="User"/> class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="logoUrl">The URL of the user's logo.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <param name="createdAtUtc">The UTC timestamp when the user was created.</param>
    /// <returns></returns>
    public static Result<User> Create(
        UserId id,
        Email email,
        string logoUrl,
        string passwordHash,
        string username,
        DateTime createdAtUtc)
    {
        return Result.Success(new User(id, email, logoUrl, passwordHash, username, createdAtUtc));
    }
}
