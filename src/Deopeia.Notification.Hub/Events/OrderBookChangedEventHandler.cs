// using Deopeia.Notification.Hub.RealTime;

// namespace Deopeia.Notification.Hub.Events;

// public class OrderBookChangedEventHandler(IHubContext<RealTimeHub, IRealTime> hubContext)
//     : IEventHandler<OrderBookChangedEvent>
// {
//     private readonly IHubContext<RealTimeHub, IRealTime> _hubContext = hubContext;

//     public async Task Handle(OrderBookChangedEvent @event)
//     {
//         await _hubContext
//             .Clients.Group(@event.Symbol)
//             .ReceiveOrderBook(@event.Symbol, @event.Bids, @event.Asks);
//     }
// }
