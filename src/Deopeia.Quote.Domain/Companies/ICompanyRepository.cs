namespace Deopeia.Quote.Domain.Companies;

public interface ICompanyRepository : IRepository<Company>
{
    Task AddAsync(IEnumerable<Company> companies);
}
