using Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

namespace Deopeia.Quote.Infrastructure.Ohlcvs.ScrapeRealTimeData;

internal class TwseScraper(HttpClient httpClient) : IRealTimeScraper
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ICollection<OhlcvDto>> GetOhlcvsAsync(IEnumerable<string> symbols)
    {
        using var content = new FormUrlEncodedContent(
            new Dictionary<string, string>()
            {
                { "json", "1" },
                { "delay", "0" },
                { "ex_ch", string.Join("|", symbols.Select(x => $"tse_{x}.tw")) },
            }
        );
        var uriBuilder = new UriBuilder("https://mis.twse.com.tw/stock/api/getStockInfo.jsp")
        {
            Query = await content.ReadAsStringAsync()
        };
        var json = await _httpClient.GetStringAsync(uriBuilder.ToString());
        var jsonArray = JsonNode.Parse(json)?.AsObject()?["msgArray"]?.AsArray();
        if (jsonArray is null)
        {
            return new List<OhlcvDto>();
        }

        var results = jsonArray
            .Select(x => x!.AsObject())
            .Select(x => new OhlcvDto
            {
                Symbol = x["c"]!.GetValue<string>(),
                DateTime = new DateTimeOffset(
                    x["tlong"]!.GetValue<string>().ToLong(),
                    TimeSpan.FromHours(8)
                ),
                Open = x["o"]!.GetValue<string>().ToDecimal(),
                High = x["h"]!.GetValue<string>().ToDecimal(),
                Low = x["l"]!.GetValue<string>().ToDecimal(),
                Close = x["z"]!.GetValue<string>().ToDecimal(),
                Volume = x["tv"]!.GetValue<string>().ToDecimal(),
            })
            .ToList();

        return results;
    }
}
