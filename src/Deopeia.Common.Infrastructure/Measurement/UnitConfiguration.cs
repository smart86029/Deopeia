using Deopeia.Common.Domain.Measurement;

namespace Deopeia.Common.Infrastructure.Measurement;

internal class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.Property(x => x.Id).HasColumnName("code");
    }
}
