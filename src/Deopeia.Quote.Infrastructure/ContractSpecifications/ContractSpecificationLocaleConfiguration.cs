using Deopeia.Quote.Domain.ContractSpecifications;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications;

internal class ContractSpecificationLocaleConfiguration
    : EntityLocaleConfiguration<
        ContractSpecification,
        ContractSpecificationLocale,
        ContractSpecificationId
    >
{
    public override void Configure(EntityTypeBuilder<ContractSpecificationLocale> builder)
    {
        base.Configure(builder);
    }
}
