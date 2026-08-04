namespace MediaBoomService.Domain.Users.ValueObjects;

public class UserId
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserId NewUserId() => new(Guid.NewGuid());

    public static UserId Empty() => new(Guid.Empty);

    public static UserId FromValue(Guid value) => new(value);

    public static implicit operator Guid(UserId userId) => userId.Value;
}
