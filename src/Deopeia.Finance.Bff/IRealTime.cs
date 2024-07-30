using Deopeia.Finance.Bff.Events;

namespace Deopeia.Finance.Bff;

public interface IRealTime
{
    Task ReceiveQuote(PriceChangedEvent @event);
}
