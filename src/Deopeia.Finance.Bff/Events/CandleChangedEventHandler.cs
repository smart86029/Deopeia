using Deopeia.Finance.Bff.Models.RealTime;

namespace Deopeia.Finance.Bff.Events;

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
                [
                    new Candle(
                        @event.TimeFrame,
                        @event.CreatedAt,
                        @event.Open,
                        @event.High,
                        @event.Low,
                        @event.Close,
                        @event.Volume
                    ),
                ]
            );
    }
}
