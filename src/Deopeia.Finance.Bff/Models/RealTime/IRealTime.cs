using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff.Models.RealTime;

public interface IRealTime
{
    Task ReceiveTick(string symbol, Tick tick);

    Task ReceiveOrderBook(Order[] bids, Order[] asks);
}
