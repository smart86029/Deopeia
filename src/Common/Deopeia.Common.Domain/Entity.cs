namespace Deopeia.Common.Domain;

public abstract class Entity<TEntityId>(TEntityId id) : IHasEvents
    where TEntityId : struct, IEntityId
{
    private readonly List<DomainEvent> _domainEvents = [];

    protected Entity()
        : this(new TEntityId()) { }

    public TEntityId Id { get; private init; } = id;

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
