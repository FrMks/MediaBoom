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
        Status = OutboxMessageStatus.Pending;
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
    /// Gets the current publishing status of the message.
    /// </summary>
    public OutboxMessageStatus Status { get; private set; }

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
    /// Marks the message as currently being published.
    /// </summary>
    public void MarkAsProcessing()
    {
        Status = OutboxMessageStatus.Processing;
    }

    /// <summary>
    /// Marks the message as successfully published.
    /// </summary>
    /// <param name="processedAtUtc">The UTC timestamp of successful publishing.</param>
    public void MarkAsProcessed(DateTime processedAtUtc)
    {
        Status = OutboxMessageStatus.Processed;
        ProcessedAtUtc = processedAtUtc;
    }

    /// <summary>
    /// Increases the retry count and returns the message to the pending state.
    /// </summary>
    public void ScheduleRetry()
    {
        RetryCount++;
        Status = OutboxMessageStatus.Pending;
    }

    /// <summary>
    /// Marks the message as permanently failed.
    /// </summary>
    public void MarkAsFailed()
    {
        Status = OutboxMessageStatus.Failed;
    }

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
