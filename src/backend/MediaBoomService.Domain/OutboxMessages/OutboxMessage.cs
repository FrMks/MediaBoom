using CSharpFunctionalExtensions;

namespace MediaBoomService.Domain.OutboxMessages;

/// <summary>
/// Represents a message waiting to be published to the message broker.
/// </summary>
public sealed class OutboxMessage
{
    // EF Core
    private OutboxMessage() { }

    private OutboxMessage(
        Guid id,
        string messageType,
        string payload,
        DateTime occurredAtUtc)
    {
        Id = id;
        MessageType = messageType;
        Payload = payload;
        OccurredAtUtc = occurredAtUtc;
        RetryCount = 0;
    }

    /// <summary>
    /// Gets the unique identifier of the outbox message.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the event type used to select the message contract.
    /// </summary>
    public string MessageType { get; private set; } = null!;

    /// <summary>
    /// Gets the serialized event payload.
    /// </summary>
    public string Payload { get; private set; } = null!;

    /// <summary>
    /// Gets the UTC timestamp when the message was created.
    /// </summary>
    public DateTime OccurredAtUtc { get; private set; }

    /// <summary>
    /// Gets the UTC timestamp when the message was successfully published.
    /// </summary>
    public DateTime? ProcessedAtUtc { get; private set; }

    /// <summary>
    /// Gets the number of publishing attempts.
    /// </summary>
    public int RetryCount { get; private set; }

    /// <summary>
    /// Creates a new unprocessed outbox message.
    /// </summary>
    /// <param name="messageType">The event type.</param>
    /// <param name="payload">The serialized event payload.</param>
    /// <param name="occurredAtUtc">The UTC creation timestamp.</param>
    /// <returns>A successful result containing the created message.</returns>
    public static Result<OutboxMessage> Create(
        string messageType,
        string payload,
        DateTime occurredAtUtc)
    {
        return Result.Success(new OutboxMessage(
            Guid.NewGuid(),
            messageType,
            payload,
            occurredAtUtc));
    }
}
