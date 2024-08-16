namespace Deopeia.Quote.Domain.Companies;

public interface ICompanyRepository : IRepository<Company, CompanyId>
{
    Task AddAsync(IEnumerable<Company> companies);
}
