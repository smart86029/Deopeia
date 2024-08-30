using Deopeia.Quote.Application.Candles.ScrapeHistoricalData;

namespace Deopeia.Quote.Infrastructure.Candles.ScrapeHistoricalData;

internal class TwseScraper(HttpClient httpClient) : IScraper
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ICollection<CandleDto>> GetOhlcvsAsync(DateOnly date)
    {
        using var content = new FormUrlEncodedContent(
            new Dictionary<string, string>()
            {
                { "date", date.ToString("yyyyMMdd") },
                { "response", "json" },
                { "type", "ALL" },
                { "_", (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 500).ToString() },
            }
        );
        var uriBuilder = new UriBuilder("http://www.twse.com.tw/exchangeReport/MI_INDEX")
        {
            Query = await content.ReadAsStringAsync()
        };
        var json = await _httpClient.GetStringAsync(uriBuilder.ToString());
        var jsonArray = JsonNode.Parse(json)?.AsObject()?["data9"]?.AsArray();
        if (jsonArray is null)
        {
            return new List<CandleDto>();
        }

        var results = jsonArray
            .Select(x => x!.AsArray())
            .Select(x => new CandleDto
            {
                Symbol = x[0]!.GetValue<string>(),
                Date = date,
                Open = x[5]!.GetValue<string>().ToDecimal(),
                High = x[6]!.GetValue<string>().ToDecimal(),
                Low = x[7]!.GetValue<string>().ToDecimal(),
                Close = x[8]!.GetValue<string>().ToDecimal(),
                Volume = x[2]!.GetValue<string>().ToDecimal(),
            })
            .ToList();

        return results;
    }
}
