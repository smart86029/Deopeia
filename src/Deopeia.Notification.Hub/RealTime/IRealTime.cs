namespace Deopeia.Notification.Hub.RealTime;

public interface IRealTime
{
    Task ReceiveTick(string symbol, Tick tick);

    Task ReceiveCandle(string symbol, int TimeFrame, Candle candle);

    // Task ReceiveOrderBook(string symbol, Order[] bids, Order[] asks);
}
