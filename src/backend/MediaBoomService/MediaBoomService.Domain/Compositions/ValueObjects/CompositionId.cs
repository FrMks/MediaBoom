namespace MediaBoomService.Domain.Compositions.ValueObjects;

/// <summary>
/// Represents a unique identifier for a composition.
/// </summary>
public class CompositionId
{
    private CompositionId(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of the composition identifier.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="CompositionId"/> class with a new unique identifier.
    /// </summary>
    /// <returns>The new <see cref="CompositionId"/> instance.</returns>
    public static CompositionId NewCompositionId() => new(Guid.NewGuid());

    /// <summary>
    /// Creates a new instance of the <see cref="CompositionId"/> class with an empty identifier.
    /// </summary>
    /// <returns>The new <see cref="CompositionId"/> instance.</returns>
    public static CompositionId Empty() => new(Guid.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="CompositionId"/> class from the specified value.
    /// </summary>
    /// <param name="value">The value for the composition identifier.</param>
    /// <returns>The new <see cref="CompositionId"/> instance.</returns>
    public static CompositionId FromValue(Guid value) => new(value);

    /// <summary>
    /// Defines an implicit conversion from <see cref="CompositionId"/> to <see cref="Guid"/>.
    /// </summary>
    /// <param name="compositionId">The composition identifier.</param>
    /// <returns>The GUID value.</returns>
    public static implicit operator Guid(CompositionId compositionId) => compositionId.Value;
}
