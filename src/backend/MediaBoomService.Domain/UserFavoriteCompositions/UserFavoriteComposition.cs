using CSharpFunctionalExtensions;
using MediaBoomService.Domain.Compositions.ValueObjects;
using MediaBoomService.Domain.UserFavoriteCompositions.ValueObjects;
using MediaBoomService.Domain.Users.ValueObjects;

namespace MediaBoomService.Domain.UserFavoriteCompositions;

/// <summary>
/// Represents a user's favorite composition in the system.
/// </summary>
public sealed class UserFavoriteComposition
{
    // Ef core
    private UserFavoriteComposition() { }

    private UserFavoriteComposition(
        UserFavoriteCompositionId userFavoriteCompositionId,
        UserId userId,
        CompositionId compositionId)
    {
        UserFavoriteCompositionId = userFavoriteCompositionId;
        UserId = userId;
        CompositionId = compositionId;
    }

    /// <summary>
    /// Gets the unique identifier of the user favorite composition.
    /// </summary>
    public UserFavoriteCompositionId UserFavoriteCompositionId { get; private set; } = null!;

    /// <summary>
    /// Gets the unique identifier of the user who favorited the composition.
    /// </summary>
    public UserId UserId { get; private set; } = null!;

    /// <summary>
    /// Gets the unique identifier of the composition that is favorited by the user.
    /// </summary>
    public CompositionId CompositionId { get; private set; } = null!;

    /// <summary>
    /// Creates a new instance of the <see cref="UserFavoriteComposition"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="compositionId">The composition identifier.</param>
    /// <returns>The created user favorite composition.</returns>
    public static Result<UserFavoriteComposition> Create(
        UserId userId,
        CompositionId compositionId)
    {
        return Result.Success(new UserFavoriteComposition(
            UserFavoriteCompositionId.NewUserFavoriteCompositionId(),
            userId,
            compositionId));
    }
}
