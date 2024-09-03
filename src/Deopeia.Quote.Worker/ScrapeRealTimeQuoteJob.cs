using Deopeia.Quote.Application.Candles.ScrapeRealTimeData;

namespace Deopeia.Quote.Worker;

internal class ScrapeRealTimeQuoteJob(ISender sender) : IInvocable
{
    private readonly ISender _sender = sender;

    public async Task Invoke()
    {
        var command = new ScrapeRealTimeDataCommand();
        await _sender.Send(command);
    }
}
