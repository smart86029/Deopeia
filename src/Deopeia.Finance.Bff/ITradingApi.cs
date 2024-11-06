using Deopeia.Finance.Bff.Models.Positions;

namespace Deopeia.Finance.Bff;

public interface ITradingApi
{
    [Get("/api/Positions")]
    Task<PageResult<PositionDto>> GetPositionsAsync(GetPositionsQuery query);
}
