namespace Deopeia.Quote.Application.ContractSpecifications.UpdateContractSpecification;

public record UpdateContractSpecificationCommand(string Mic, string TimeZone) : IRequest { }
