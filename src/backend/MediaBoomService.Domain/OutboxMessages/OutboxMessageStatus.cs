namespace MediaBoomService.Domain.OutboxMessages;

/// <summary>
/// Represents the publishing state of an outbox message.
/// </summary>
public enum OutboxMessageStatus
{
    Pending,
    Processing,
    Processed,
    Failed
}
