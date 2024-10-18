using Deopeia.Trading.Domain.Orders.LimitOrders;

namespace Deopeia.Trading.Infrastructure.Orders.LimitOrders;

internal class LimitOrderConfiguration : IEntityTypeConfiguration<LimitOrder>
{
    public void Configure(EntityTypeBuilder<LimitOrder> builder)
    {
        builder.ComplexProperty(x => x.LimitPrice);
    }
}
