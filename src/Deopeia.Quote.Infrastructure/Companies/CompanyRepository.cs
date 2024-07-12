using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Infrastructure.Companies;

internal class CompanyRepository(QuoteContext context) : ICompanyRepository
{
    private readonly DbSet<Company> _companies = context.Set<Company>();

    public async Task AddAsync(IEnumerable<Company> companies)
    {
        await _companies.AddRangeAsync(companies);
    }
}
