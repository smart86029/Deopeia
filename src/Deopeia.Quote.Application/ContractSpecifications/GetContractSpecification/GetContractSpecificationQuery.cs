namespace Deopeia.Quote.Application.ContractSpecifications.GetContractSpecification;

public record GetContractSpecificationQuery(Guid Id)
    : IRequest<GetContractSpecificationViewModel> { }
