using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TraderConfiguration : IEntityTypeConfiguration<Trader>
{
    public void Configure(EntityTypeBuilder<Trader> builder) { }
}
