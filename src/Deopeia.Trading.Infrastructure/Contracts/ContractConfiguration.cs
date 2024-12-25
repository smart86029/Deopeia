using Deopeia.Common.Infrastructure.Comparers;
using Deopeia.Common.Infrastructure.Converters;
using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Infrastructure.Contracts;

internal class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.Property(x => x.Id).HasColumnName(nameof(Symbol).ToSnakeCaseLower());

        builder.ComplexProperty(x => x.ContractSize);

        builder.ComplexProperty(x => x.VolumeRestriction);

        builder
            .Property(x => x.Leverages)
            .HasConversion<JsonConverter<IReadOnlyCollection<decimal>>>(
                new EnumerableComparer<decimal>()
            )
            .HasColumnType("jsonb");

        builder.HasMany(x => x.Sessions).WithOne().HasForeignKey(x => x.Symbol);
    }
}
