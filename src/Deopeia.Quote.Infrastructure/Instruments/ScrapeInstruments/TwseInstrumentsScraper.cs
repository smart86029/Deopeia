using Deopeia.Quote.Application.Instruments.ScrapeInstruments;

namespace Deopeia.Quote.Infrastructure.Instruments.ScrapeInstruments;

internal class TwseInstrumentsScraper(HttpClient httpClient) : IInstrumentsScraper
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ICollection<InstrumentDto>> GetInstrumentsAsync()
    {
        var uri = new Uri("https://openapi.twse.com.tw/v1/opendata/t187ap03_L");
        var json = await _httpClient.GetStringAsync(uri);
        var jsonArray = JsonNode.Parse(json)?.AsArray();
        if (jsonArray is null)
        {
            return new List<InstrumentDto>();
        }

        var results = jsonArray
            .Select(x => x!.AsObject())
            .Select(x => new InstrumentDto
            {
                Symbol = x["公司代號"]!.GetValue<string>(),
                CompanyName = x["公司名稱"]!.GetValue<string>(),
                Name = x["公司簡稱"]!.GetValue<string>(),
                EnglishName = x["英文簡稱"]!.GetValue<string>(),
                Industry = x["產業別"]!.GetValue<string>(),
                Website = x["網址"]!.GetValue<string>(),
            })
            .ToList();

        return results;
    }
}
