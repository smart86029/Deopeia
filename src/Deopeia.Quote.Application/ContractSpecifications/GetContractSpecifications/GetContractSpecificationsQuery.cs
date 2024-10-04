namespace Deopeia.Quote.Application.ContractSpecifications.GetContractSpecifications;

public record GetContractSpecificationsQuery(string? ExchangeId, Guid? AssetId)
    : PageQuery<ContractSpecificationDto> { }
