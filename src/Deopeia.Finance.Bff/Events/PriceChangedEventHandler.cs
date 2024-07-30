namespace Deopeia.Finance.Bff.Events;

public class PriceChangedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
    : IEventHandler<PriceChangedEvent>
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    public async Task Handle(PriceChangedEvent @event)
    {
        await _hubContext.Clients.All.ReceiveQuote(@event);
    }
}
