using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Orders.LimitOrders;
using Deopeia.Trading.Domain.Orders.MarketOrders;
using Deopeia.Trading.Domain.Orders.StopOrders;

namespace Deopeia.Trading.Infrastructure.Orders;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<MarketOrder>(OrderType.Market)
            .HasValue<LimitOrder>(OrderType.Limit)
            .HasValue<StopOrder>(OrderType.Stop);

        builder.Ignore(x => x.UnfilledVolume);

        builder.ComplexProperty(x => x.Price);
    }
}
