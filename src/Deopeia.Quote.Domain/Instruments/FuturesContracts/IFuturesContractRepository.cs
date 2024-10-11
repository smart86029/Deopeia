namespace Deopeia.Quote.Domain.Instruments.FuturesContracts;

public interface IFuturesContractRepository : IRepository<FuturesContract, InstrumentId>
{
    Task<FuturesContract> GetFuturesContractAsync(InstrumentId id);

    Task AddAsync(FuturesContract futuresContract);

    Task<bool> Exists(string symbol);
}
