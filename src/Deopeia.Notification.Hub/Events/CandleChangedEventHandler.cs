using Deopeia.Notification.Hub.RealTime;

namespace Deopeia.Notification.Hub.Events;

public class CandleChangedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
    : IEventHandler<CandleChangedEvent>
{
    private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

    public async Task Handle(CandleChangedEvent @event)
    {
        await _hubContext
            .Clients.Group(@event.Symbol)
            .ReceiveCandle(
                @event.Symbol,
                @event.TimeFrame,
                new Candle(
                    @event.Timestamp,
                    @event.Open,
                    @event.High,
                    @event.Low,
                    @event.Close,
                    @event.Volume
                )
            );
    }
}
