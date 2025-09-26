namespace Deopeia.Product.Domain.Instruments.Spots;

public interface ISpotRepository : IRepository<Spot, InstrumentId>
{
    Task<Spot> GetSpotAsync(InstrumentId instrumentId);

    void Add(Spot spot);

    void Remove(Spot spot);
}
