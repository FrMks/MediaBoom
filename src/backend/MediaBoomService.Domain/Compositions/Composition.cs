using CSharpFunctionalExtensions;
using MediaBoomService.Domain.Compositions.ValueObjects;
using MediaBoomService.Domain.Users.ValueObjects;

namespace MediaBoomService.Domain.Compositions;

public sealed class Composition
{
    // Ef core
    private Composition() { }

    private Composition(
        CompositionId id,
        UserId uploadedByUserId,
        Guid fileId,
        Title title,
        ArtistName artistName,
        DateTime publishedAtUtc)
    {
        Id = id;
        UploadedByUserId = uploadedByUserId;
        FileId = fileId;
        Title = title;
        ArtistName = artistName;
        PublishedAtUtc = publishedAtUtc;
    }

    /// <summary>
    /// Gets the unique identifier of the composition.
    /// </summary>
    public CompositionId Id { get; private set; } = null!;

    /// <summary>
    /// Gets the identifier of the user who uploaded the composition.
    /// </summary>
    public UserId UploadedByUserId { get; private set; } = null!;

    /// <summary>
    /// Gets the identifier of the uploaded file.
    /// </summary>
    public Guid FileId { get; private set; }

    /// <summary>
    /// Gets the title of the composition.
    /// </summary>
    public Title Title { get; private set; } = null!;

    /// <summary>
    /// Gets the name of the composition's artist.
    /// </summary>
    public ArtistName ArtistName { get; private set; } = null!;

    /// <summary>
    /// Gets the UTC timestamp when the composition was published.
    /// </summary>
    public DateTime PublishedAtUtc { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Composition"/> class.
    /// </summary>
    /// <param name="uploadedByUserId">The unique identifier of the user who uploaded the composition.</param>
    /// <param name="fileId">The unique identifier of the file.</param>
    /// <param name="title">The title of the composition.</param>
    /// <param name="artistName">The name of the artist.</param>
    /// <param name="publishedAtUtc">The UTC timestamp when the composition was published.</param>
    /// <returns>A successful result containing the created composition.</returns>
    public static Result<Composition> Create(
        UserId uploadedByUserId,
        Guid fileId,
        Title title,
        ArtistName artistName,
        DateTime publishedAtUtc)
    {
        return Result.Success(new Composition(
            CompositionId.NewCompositionId(),
            uploadedByUserId,
            fileId,
            title,
            artistName,
            publishedAtUtc));
    }
}
