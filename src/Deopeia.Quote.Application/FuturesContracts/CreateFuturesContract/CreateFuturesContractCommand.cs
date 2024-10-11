namespace Deopeia.Quote.Application.FuturesContracts.CreateFuturesContract;

public record CreateFuturesContractCommand(Guid ContractSpecificationId, DateOnly ExpirationDate)
    : IRequest { }
