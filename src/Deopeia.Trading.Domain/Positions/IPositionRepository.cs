namespace Deopeia.Trading.Domain.Positions;

public interface IPositionRepository : IRepository<Position, PositionId>
{
    Task AddAsync(Position position);
}
