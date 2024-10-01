using Deopeia.Quote.Domain.ContractSpecifications;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications;

internal class ContractSpecificationIdConverter()
    : ValueConverter<ContractSpecificationId, Guid>(
        id => id.Guid,
        guid => new ContractSpecificationId(guid)
    ) { }
