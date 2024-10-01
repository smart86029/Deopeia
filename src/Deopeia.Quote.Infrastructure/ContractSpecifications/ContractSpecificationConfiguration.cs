using Deopeia.Quote.Domain.ContractSpecifications;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications;

internal class ContractSpecificationConfiguration : IEntityTypeConfiguration<ContractSpecification>
{
    public void Configure(EntityTypeBuilder<ContractSpecification> builder)
    {
        builder.ComplexProperty(x => x.ContractSize);
    }
}
