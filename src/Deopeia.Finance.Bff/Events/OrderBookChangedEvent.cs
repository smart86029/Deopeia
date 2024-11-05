using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff.Events;

public record OrderBookChangedEvent(string Symbol, Order[] Bids, Order[] Asks) : Event { }
