namespace Deopeia.Common.Domain;

public interface IRepository<TAggregateRoot, TEntityId>
    where TAggregateRoot : AggregateRoot<TEntityId>
    where TEntityId : struct, IEntityId { }
