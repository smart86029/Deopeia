using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Infrastructure.Contracts;

internal class ContractRepository(TradingContext context) : IContractRepository
{
    private readonly DbSet<Contract> _contracts = context.Set<Contract>();

    public async Task<ICollection<Contract>> GetContractsAsync()
    {
        return await _contracts.Include(x => x.Sessions).Include(x => x.Locales).ToListAsync();
    }

    public async Task<Contract> GetContractAsync(Symbol symbol)
    {
        return await _contracts
            .Include(x => x.Sessions)
            .Include(x => x.Locales)
            .FirstAsync(x => x.Id == symbol);
    }

    public async Task<bool> ExistsAsync(Symbol symbol)
    {
        return await _contracts.AnyAsync(x => x.Id == symbol);
    }

    public async Task AddAsync(Contract contract)
    {
        await _contracts.AddAsync(contract);
    }
}
