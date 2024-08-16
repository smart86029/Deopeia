namespace Deopeia.Common.Domain;

public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>
    where TEntityId : struct, IEntityId
{
    protected AggregateRoot() { }

    protected AggregateRoot(TEntityId id)
        : base(id) { }
}
