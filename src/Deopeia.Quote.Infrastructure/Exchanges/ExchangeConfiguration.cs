using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges;

internal class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
