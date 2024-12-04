using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Infrastructure.Contracts;

internal class ContractLocaleConfiguration
    : EntityLocaleConfiguration<Contract, ContractLocale, Symbol>
{
    public override void Configure(EntityTypeBuilder<ContractLocale> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(Symbol).ToSnakeCaseLower());
    }
}
