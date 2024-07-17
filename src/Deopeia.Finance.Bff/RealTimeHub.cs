namespace Deopeia.Finance.Bff;

public class RealTimeHub : Hub<IRealTime>
{
    public async Task SendQuote(string quote)
    {
        await Clients.All.ReceiveQuote(quote);
    }
}
