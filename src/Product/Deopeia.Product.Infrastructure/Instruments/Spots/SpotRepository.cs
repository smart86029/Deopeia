using Deopeia.Product.Domain.Instruments;
using Deopeia.Product.Domain.Instruments.Spots;

namespace Deopeia.Product.Infrastructure.Instruments.Spots;

internal sealed class SpotRepository(ProductContext context) : ISpotRepository
{
    private readonly DbSet<Spot> _spots = context.Set<Spot>();

    public Task<Spot> GetSpotAsync(InstrumentId instrumentId)
    {
        return _spots.Include(x => x.Localizations).FirstAsync(x => x.Id == instrumentId);
    }

    public void Add(Spot spot)
    {
        _spots.Add(spot);
    }

    public void Remove(Spot spot)
    {
        _spots.Remove(spot);
    }
}
