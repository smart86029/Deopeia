namespace Deopeia.Trading.Domain.Traders;

public interface ITraderRepository : IRepository<Trader, TraderId>
{
    Task<IReadOnlyList<Trader>> GetTradersAsync();

    Task<Trader> GetTraderAsync(TraderId id);

    Task AddAsync(Trader Trader);
}
