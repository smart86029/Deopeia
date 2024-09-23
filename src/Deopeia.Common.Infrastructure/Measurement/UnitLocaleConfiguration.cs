using Deopeia.Common.Domain.Measurement;
using Deopeia.Common.Infrastructure.Localization;

namespace Deopeia.Common.Infrastructure.Measurement;

internal class UnitLocaleConfiguration : EntityLocaleConfiguration<Unit, UnitLocale, UnitCode>
{
    public override void Configure(EntityTypeBuilder<UnitLocale> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(UnitCode).ToSnakeCaseLower());
    }
}
