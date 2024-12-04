namespace Deopeia.Trading.Application.Contracts.GetContractOptions;

public record GetContractOptionsQuery(UnderlyingType UnderlyingType) : OptionsQuery<string> { }
