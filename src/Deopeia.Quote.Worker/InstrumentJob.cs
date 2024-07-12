using Deopeia.Quote.Application.Instruments.ScrapeInstruments;

namespace Deopeia.Quote.Worker;

internal class InstrumentJob(ISender sender) : IInvocable
{
    private readonly ISender _sender = sender;

    public async Task Invoke()
    {
        var command = new ScrapeInstrumentsCommand();
        await _sender.Send(command);
    }
}
