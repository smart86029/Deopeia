using Deopeia.Finance.Bff.Models.RealTime;

namespace Deopeia.Finance.Bff.Events;

public class PriceChangedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
    : IEventHandler<PriceChangedEvent>
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    public async Task Handle(PriceChangedEvent @event)
    {
        var tick = new Tick(
            @event.CreatedAt.UtcTicks,
            @event.Price,
            @event.Volume,
            @event.Bid,
            @event.Ask
        );
        await _hubContext.Clients.All.ReceiveTick(@event.Symbol, tick);
    }
}
