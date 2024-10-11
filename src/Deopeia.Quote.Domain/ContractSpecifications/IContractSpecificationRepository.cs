namespace Deopeia.Quote.Domain.ContractSpecifications;

public interface IContractSpecificationRepository
    : IRepository<ContractSpecification, ContractSpecificationId>
{
    Task<ContractSpecification> GetContractSpecificationAsync(ContractSpecificationId id);

    Task AddAsync(ContractSpecification contractSpecification);

    Task<bool> Exists(string symbol);
}