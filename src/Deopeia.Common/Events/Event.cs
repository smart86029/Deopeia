namespace Deopeia.Common.Events;

public abstract record Event
{
    public Guid Id { get; private init; } = Guid.NewGuid();

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;
}
