namespace Deopeia.Quote.Domain.ContractSpecifications;

public readonly record struct ContractSpecificationId(Guid Guid) : IEntityId
{
    public ContractSpecificationId()
        : this(Guid.CreateVersion7()) { }
}
