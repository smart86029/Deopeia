namespace Deopeia.Quote.Application.Candles.MockRealTimeData;

public record OrderBookChangedEvent(string Symbol, OrderDto[] Bids, OrderDto[] Asks) : Event { }
