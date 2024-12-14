using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff.Models.RealTime;

public interface IRealTime
{
    Task ReceiveTick(string symbol, Tick tick);

    Task ReceiveCandle(string symbol, Candle[] candles);

    Task ReceiveOrderBook(Order[] bids, Order[] asks);
}
