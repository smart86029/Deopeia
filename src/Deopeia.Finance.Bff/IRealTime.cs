using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff;

public interface IRealTime
{
    Task ChangeSymbol(string symbol);

    Task ReceiveQuote(PriceChangedEvent @event);

    Task ReceiveOrderBook(Order[] bids, Order[] asks);
}
