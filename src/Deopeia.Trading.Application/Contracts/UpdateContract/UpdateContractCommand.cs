namespace Deopeia.Trading.Application.Contracts.UpdateContract;

public record UpdateContractCommand(string Symbol, ICollection<ContractLocaleDto> Locales)
    : IRequest { }
