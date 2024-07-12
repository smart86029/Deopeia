using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Application.Instruments.ScrapeInstruments;

internal class ScrapeInstrumentsCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    ICompanyRepository companyRepository,
    IStockRepository stockRepository,
    IInstrumentsScraper instrumentsScraper
) : IRequestHandler<ScrapeInstrumentsCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICompanyRepository _companyRepository = companyRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IInstrumentsScraper _instrumentsScraper = instrumentsScraper;

    public async Task Handle(ScrapeInstrumentsCommand request, CancellationToken cancellationToken)
    {
        var items = await _instrumentsScraper.GetInstrumentsAsync();
        var enUS = CultureInfo.GetCultureInfo("en-US");
        var zhTW = CultureInfo.GetCultureInfo("zh-TW");

        var companies = new List<Company>(items.Count);
        var stocks = new List<Stock>(items.Count);
        foreach (var item in items)
        {
            var subIndustry = GetSubIndustry(item.Industry);
            var website = GetWebsite(item.Website);
            var company = new Company(subIndustry, website);
            company.UpdateName(item.CompanyName, zhTW);
            companies.Add(company);

            var stock = new Stock(item.Symbol, company.Id);
            stock.UpdateName(item.EnglishName, enUS);
            stock.UpdateName(item.Name, zhTW);
            stocks.Add(stock);
        }

        await _companyRepository.AddAsync(companies);
        await _stockRepository.AddAsync(stocks);
        await _unitOfWork.CommitAsync();
    }

    private SubIndustry GetSubIndustry(string industry)
    {
        return industry switch
        {
            "24" => SubIndustry.Semiconductors,
            _ => SubIndustry.OilAndGasDrilling,
        };
    }

    private Uri? GetWebsite(string website)
    {
        if (website.IsNullOrWhiteSpace())
        {
            return null;
        }

        website = website
            .ToLower()
            .Replace(" ", string.Empty)
            .Replace("http//", "http://")
            .Replace("http://", "https://")
            .Replace("https//", "https://");
        if (!website.StartsWith("http"))
        {
            website = $"https://{website}";
        }

        return new Uri(website);
    }
}
