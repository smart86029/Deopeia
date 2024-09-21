namespace Deopeia.Quote.Application.FuturesContracts.GetFuturesContractOptions;

public record GetFuturesContractOptionsQuery(Guid AssetId) : OptionsQuery<Guid> { }
