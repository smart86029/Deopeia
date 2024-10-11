using Deopeia.Quote.Domain.ContractSpecifications;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications;

internal class ContractSpecificationRepository(QuoteContext context)
    : IContractSpecificationRepository
{
    private readonly DbSet<ContractSpecification> _contractSpecifications =
        context.Set<ContractSpecification>();

    public async Task<ContractSpecification> GetContractSpecificationAsync(
        ContractSpecificationId id
    )
    {
        return await _contractSpecifications.Include(x => x.Locales).FirstAsync(x => x.Id == id);
    }

    public async Task AddAsync(ContractSpecification contractSpecification)
    {
        await _contractSpecifications.AddAsync(contractSpecification);
    }

    public async Task<bool> Exists(string symbol)
    {
        return await _contractSpecifications.AnyAsync(x => x.Symbol == symbol);
    }
}
