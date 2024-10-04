namespace Deopeia.Quote.Application.ContractSpecifications.GetContractSpecificationOptions;

public record GetContractSpecificationOptionsQuery(Guid AssetId) : OptionsQuery<Guid> { }
