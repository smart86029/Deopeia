using Deopeia.Quote.Application.Candles.ScrapeRealTimeData;

namespace Deopeia.Quote.Infrastructure.Candles.ScrapeRealTimeData;

internal class TwseScraper(HttpClient httpClient) : IRealTimeScraper
{
    private static readonly TimeSpan Offset = TimeSpan.FromHours(8);

    private readonly HttpClient _httpClient = httpClient;

    public async Task<ICollection<RealTimeDto>> GetRealTimeDataAsync(IEnumerable<string> symbols)
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
            return new List<RealTimeDto>();
        }

        var results = jsonArray
            .Select(x => x!.AsObject())
            .Select(x => new RealTimeDto
            {
                Symbol = x["c"]!.GetValue<string>(),
                LastTradedAt = DateTimeOffset
                    .FromUnixTimeMilliseconds(x["tlong"]!.GetValue<string>().ToLong())
                    .ToOffset(Offset),
                LastTradedPrice = x["z"]!.GetValue<string>().ToDecimal(),
                PreviousClose = x["y"]!.GetValue<string>().ToDecimal(),
            })
            .ToList();

        return results;
    }
}
