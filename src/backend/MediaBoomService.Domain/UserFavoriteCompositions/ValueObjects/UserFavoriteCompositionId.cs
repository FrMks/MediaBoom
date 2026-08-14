namespace MediaBoomService.Domain.UserFavoriteCompositions.ValueObjects;

/// <summary>
/// Represents a unique identifier for a user's favorite composition.
/// </summary>
public class UserFavoriteCompositionId
{
    private UserFavoriteCompositionId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of the user favorite composition identifier.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="UserFavoriteCompositionId"/> class with a new unique identifier.
    /// </summary>
    /// <returns>UserFavoriteCompositionId</returns>
    public static UserFavoriteCompositionId NewUserFavoriteCompositionId() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="UserFavoriteCompositionId"/> class with an empty identifier.
    /// </summary>
    /// <returns>UserFavoriteCompositionId</returns>
    public static UserFavoriteCompositionId Empty() => new(Guid.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="UserFavoriteCompositionId"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value for the user favorite composition identifier.</param>
    /// <returns>UserFavoriteCompositionId</returns>
    public static UserFavoriteCompositionId FromValue(Guid value) => new(value);

    /// <summary>
    /// Defines an implicit conversion from <see cref="UserFavoriteCompositionId"/> to <see cref="Guid"/>.
    /// </summary>
    /// <param name="userId">The user favorite composition identifier to convert.</param>
    /// <returns>The converted GUID.</returns>
    public static implicit operator Guid(UserFavoriteCompositionId userId) => userId.Value;
}