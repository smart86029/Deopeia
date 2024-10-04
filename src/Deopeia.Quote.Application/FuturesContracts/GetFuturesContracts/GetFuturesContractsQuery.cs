namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContracts;

public record GetFuturesContractsQuery(string? ExchangeId, Guid? AssetId)
    : PageQuery<FuturesContractDto> { }
