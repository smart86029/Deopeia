using Deopeia.Common.Domain.Finance;
using Deopeia.Common.Infrastructure.Localization;

namespace Deopeia.Common.Infrastructure.Finance;

internal class CurrencyLocaleConfiguration
    : EntityLocaleConfiguration<Currency, CurrencyLocale, CurrencyCode>
{
    public override void Configure(EntityTypeBuilder<CurrencyLocale> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.EntityId).HasColumnName(nameof(CurrencyCode).ToSnakeCaseLower());
    }
}
