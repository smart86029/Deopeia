using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Infrastructure.Accounts;

internal class AccountRepository(TradingContext context) : IAccountRepository
{
    private readonly DbSet<Account> _accounts = context.Set<Account>();

    public async Task<IReadOnlyList<Account>> GetAccountsAsync()
    {
        return await _accounts.ToListAsync();
    }

    public async Task<Account> GetAccountAsync(AccountId id)
    {
        return await _accounts.SingleAsync(x => x.Id == id);
    }

    public async Task AddAsync(Account account)
    {
        await _accounts.AddAsync(account);
    }
}
