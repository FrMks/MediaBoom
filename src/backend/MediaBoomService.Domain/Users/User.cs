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

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the URL of the user's logo.
    /// </summary>
    public string LogoUrl { get; private set; }

    /// <summary>
    /// Gets the UTC timestamp when the user was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; private set; }

    /// <summary>
    /// Gets the hashed password of the user.
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="User"/> class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="logoUrl">The URL of the user's logo.</param>
    /// <param name="createdAtUtc">The UTC timestamp when the user was created.</param>
    /// <param name="passwordHash">The hashed password of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <returns></returns>
    public static User Create(
        Guid id,
        Email email,
        string logoUrl,
        DateTime createdAtUtc,
        string passwordHash,
        string username)
    {
        return new User(id, email, logoUrl, createdAtUtc, passwordHash, username);
    }
}
