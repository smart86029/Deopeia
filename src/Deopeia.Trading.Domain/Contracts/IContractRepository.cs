namespace Deopeia.Trading.Domain.Contracts;

public interface IContractRepository : IRepository<Contract, Symbol>
{
    Task<ICollection<Contract>> GetContractsAsync();

    Task<Contract> GetContractAsync(Symbol symbol);

    Task AddAsync(Contract contract);
}
