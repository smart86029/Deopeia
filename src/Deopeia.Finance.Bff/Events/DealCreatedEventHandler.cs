using Deopeia.Finance.Bff.Models.RealTime;

namespace Deopeia.Finance.Bff.Events;

public class DealCreatedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
    : IEventHandler<DealCreatedEvent>
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    public async Task Handle(DealCreatedEvent @event)
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
