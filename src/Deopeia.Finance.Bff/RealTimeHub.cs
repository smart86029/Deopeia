namespace Deopeia.Finance.Bff;

public class RealTimeHub : Hub<IRealTime>
{
    public async Task SendQuote(PriceChangedEvent @event)
    {
        await Clients.All.ReceiveQuote(@event);
    }
}
