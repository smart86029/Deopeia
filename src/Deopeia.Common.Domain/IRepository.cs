namespace Deopeia.Common.Domain;

public interface IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot { }
