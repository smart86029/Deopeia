namespace Deopeia.Finance.Bff;

public interface IRealTime
{
    Task ReceiveQuote(PriceChangedEvent @event);
}
