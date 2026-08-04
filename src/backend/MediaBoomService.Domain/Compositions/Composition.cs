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
        string title,
        string artistName,
        DateTime publishedAtUtc)
    {
        Id = id;
        UploadedByUserId = uploadedByUserId;
        FileId = fileId;
        Title = title;
        ArtistName = artistName;
        PublishedAtUtc = publishedAtUtc;
    }

    public CompositionId Id { get; private set; } = null!;

    public UserId UploadedByUserId { get; private set; } = null!;

    public Guid FileId { get; private set; }

    public string Title { get; private set; }

    public string ArtistName { get; private set; }

    public DateTime PublishedAtUtc { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Composition"/> class.
    /// </summary>
    /// <param name="uploadedByUserId">The unique identifier of the user who uploaded the composition.</param>
    /// <param name="fileId">The unique identifier of the file.</param>
    /// <param name="title">The title of the composition.</param>
    /// <param name="artistName">The name of the artist.</param>
    /// <param name="publishedAtUtc">The UTC timestamp when the composition was published.</param>
    /// <returns></returns>
    public static Result<Composition> Create(
        UserId uploadedByUserId,
        Guid fileId,
        string title,
        string artistName,
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
