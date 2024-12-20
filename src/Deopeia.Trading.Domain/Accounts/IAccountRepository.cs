namespace Deopeia.Trading.Domain.Accounts;

public interface IAccountRepository : IRepository<Account, AccountId>
{
    Task<IReadOnlyList<Account>> GetAccountsAsync();

    Task<Account> GetAccountAsync(AccountId id);

    Task AddAsync(Account account);
}
