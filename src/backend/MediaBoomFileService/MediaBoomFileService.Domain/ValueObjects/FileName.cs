using CSharpFunctionalExtensions;
using Shared;

namespace MediaBoomFileService.Domain.ValueObjects;

/// <summary>
/// Original name of file, which uploaded user.
/// </summary>
public sealed record FileName
{
    /// <summary>
    /// Name of file without extension.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Extension on file.
    /// </summary>
    public string Extension { get; init; } = null!;

    private FileName() { }

    private FileName(string name, string extension)
    {
        Name = name;
        Extension = extension;
    }

    /// <summary>
    /// Create method for FileName with validation.
    /// </summary>
    /// <param name="fileName">Name like (name + extension).</param>
    /// <returns>Return or success value object or error.</returns>
    public static Result<FileName, Error> Create(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Error.Validation("value.is.null", nameof(fileName));

        int lastDot = fileName.LastIndexOf('.');
        if (lastDot == -1 || lastDot == fileName.Length - 1)
            return Error.Validation("file.dont.have.extension", "File must have extension");

        string extension = fileName[(lastDot + 1)..].ToLowerInvariant();
        string name = fileName[..lastDot];
        return new FileName(name, extension);
    }
}
