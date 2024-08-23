using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges;

internal class ExchangeLocaleConfiguration
    : EntityLocaleConfiguration<Exchange, ExchangeLocale, ExchangeId>
{
    public override void Configure(EntityTypeBuilder<ExchangeLocale> builder)
    {
        base.Configure(builder);
    }
}
