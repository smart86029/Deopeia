using Deopeia.Trading.Domain.Orders.StopOrders;

namespace Deopeia.Trading.Infrastructure.Orders.StopOrders;

internal class StopOrderConfiguration : IEntityTypeConfiguration<StopOrder>
{
    public void Configure(EntityTypeBuilder<StopOrder> builder)
    {
        builder.ComplexProperty(x => x.StopPrice);

        builder.HasIndex(x => x.Triggeredby);
    }
}
