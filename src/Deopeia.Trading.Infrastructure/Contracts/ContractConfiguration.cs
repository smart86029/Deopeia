using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Infrastructure.Contracts;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.Property(x => x.Id).HasColumnName(nameof(Symbol).ToSnakeCaseLower());

        builder.ComplexProperty(x => x.ContractSize);
    }
}
