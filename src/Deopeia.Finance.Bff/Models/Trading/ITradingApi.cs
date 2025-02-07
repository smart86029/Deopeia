using Deopeia.Finance.Bff.Models.Contracts;
using Deopeia.Finance.Bff.Models.Positions;

namespace Deopeia.Finance.Bff.Models.Trading;

public interface ITradingApi
{
    [Get("/api/Contracts")]
    Task<PageResult<ContractDto>> GetContractsAsync(GetContractsQuery query);

    [Get("/api/Contracts/{symbol}")]
    Task<GetContractViewModel> GetContractAsync(string symbol);

    [Get("/api/Positions")]
    Task<PageResult<PositionDto>> GetPositionsAsync(GetPositionsQuery query);

    [Get("/api/Traders/{traderId}/Accounts")]
    Task<Account[]> GetAccountsAsync(Guid traderId);

    [Get("/api/Traders/{traderId}/Favorites")]
    Task<string[]> GetFavoritesAsync(Guid traderId);

    [Put("/api/Traders/{traderId}/Favorites/{symbol}")]
    Task LikeAsync(Guid traderId, string symbol);

    [Delete("/api/Traders/{traderId}/Favorites/{symbol}")]
    Task DislikeAsync(Guid traderId, string symbol);
}
