using Deopeia.Finance.Bff.Models.RealTime;

namespace Deopeia.Finance.Bff.Events;

public class OrderBookChangedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
    : IEventHandler<OrderBookChangedEvent>
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    public async Task Handle(OrderBookChangedEvent @event)
    {
        await _hubContext.Clients.Group(@event.Symbol).ReceiveOrderBook(@event.Bids, @event.Asks);
    }
}
