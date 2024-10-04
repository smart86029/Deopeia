namespace Deopeia.Quote.Application.ContractSpecifications.CreateContractSpecification;

public record CreateContractSpecificationCommand(string Mic, string TimeZone) : IRequest { }
