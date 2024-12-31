using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff.Models.RealTime;

public interface IRealTime
{
    Task ReceiveTick(string symbol, Tick tick);

    Task ReceiveCandle(string symbol, int TimeFrame, Candle candle);

    Task ReceiveOrderBook(string symbol, Order[] bids, Order[] asks);
}
