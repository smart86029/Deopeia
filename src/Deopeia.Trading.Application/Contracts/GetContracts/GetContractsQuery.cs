namespace Deopeia.Trading.Application.Contracts.GetContracts;

public record GetContractsQuery(UnderlyingType? UnderlyingType, Guid? TraderId)
    : PageQuery<ContractDto> { }
