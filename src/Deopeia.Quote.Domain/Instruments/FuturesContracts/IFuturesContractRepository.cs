namespace Deopeia.Quote.Domain.Instruments.FuturesContracts;

public interface IFuturesContractRepository : IRepository<FuturesContract, InstrumentId>
{
    Task AddAsync(IEnumerable<FuturesContract> futuresContracts);

    Task<bool> Exists(string symbol);
}
