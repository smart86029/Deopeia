using Deopeia.Finance.Bff.Models.Contracts;
using Deopeia.Finance.Bff.Models.Positions;

namespace Deopeia.Finance.Bff;

public interface ITradingApi
{
    [Get("/api/Contracts")]
    Task<PageResult<ContractDto>> GetContractsAsync(GetContractsQuery query);

    [Get("/api/Positions")]
    Task<PageResult<PositionDto>> GetPositionsAsync(GetPositionsQuery query);
}
