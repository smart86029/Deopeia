using Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;

namespace Deopeia.Quote.Worker;

internal class ScrapeJob(ISender sender) : IInvocable
{
    private readonly ISender _sender = sender;

    public async Task Invoke()
    {
        var date = DateTime.Today;
        for (var i = 0; i < 7; i++)
        {
            date = GetPreviousWorkday(date);
            var command = new ScrapeHistoricalDataCommand(date.ToDateOnly());
            await _sender.Send(command);
        }
    }

    private DateTime GetPreviousWorkday(DateTime date)
    {
        do
        {
            date = date.AddDays(-1);
        } while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);

        return date;
    }
}
