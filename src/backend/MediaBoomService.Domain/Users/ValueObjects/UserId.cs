namespace MediaBoomService.Domain.Users.ValueObjects;

/// <summary>
/// Represents a unique identifier for a user.
/// </summary>
public class UserId
{
    private UserId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of the user identifier.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="UserId"/> class with a new unique identifier.
    /// </summary>
    /// <returns>UserId</returns>
    public static UserId NewUserId() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="UserId"/> class with an empty identifier.
    /// </summary>
    /// <returns>UserId</returns>
    public static UserId Empty() => new(Guid.Empty);
    
    /// <summary>
    /// Creates a new instance of the <see cref="UserId"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value for the user identifier.</param>
    /// <returns>UserId</returns>
    public static UserId FromValue(Guid value) => new(value);

    /// <summary>
    /// Defines an implicit conversion from <see cref="UserId"/> to <see cref="Guid"/>.
    /// </summary>
    /// <param name="userId">The user identifier to convert.</param>
    /// <returns>The converted GUID.</returns>
    public static implicit operator Guid(UserId userId) => userId.Value;
}
