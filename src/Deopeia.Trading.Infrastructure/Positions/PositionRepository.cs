using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Infrastructure.Positions;

internal class PositionRepository(TradingContext context) : IPositionRepository
{
    private readonly DbSet<Position> _positions = context.Set<Position>();

    public async Task AddAsync(Position position)
    {
        await _positions.AddAsync(position);
    }
}
