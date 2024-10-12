namespace Deopeia.Trading.Domain.Accounts;

public interface IAccountRepository : IRepository<Account, AccountId>
{
    Task<Account> GetAccountAsync(AccountId id);

    Task AddAsync(Account account);
}
