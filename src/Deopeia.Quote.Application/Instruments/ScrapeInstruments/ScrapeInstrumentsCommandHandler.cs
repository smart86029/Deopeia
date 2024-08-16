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
        try
        {
            var items = await _instrumentsScraper.GetInstrumentsAsync();
            var enUS = CultureInfo.GetCultureInfo("en-US");
            var zhTW = CultureInfo.GetCultureInfo("zh-TW");

            var companies = new List<Company>(items.Count);
            var stocks = new List<Stock>(items.Count);
            foreach (var item in items)
            {
                if (await _stockRepository.Exists(item.Symbol))
                {
                    continue;
                }

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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private SubIndustry GetSubIndustry(string industry)
    {
        return industry switch
        {
            "01" => SubIndustry.ConstructionMaterials,
            "02" => SubIndustry.AgriculturalProductsAndServices,
            "03" => SubIndustry.CommodityChemicals,
            "04" => SubIndustry.Textiles,
            "05" => SubIndustry.IndustrialMachineryAndSuppliesAndComponents,
            "06" => SubIndustry.ElectricalComponentsAndEquipment,
            "08" => SubIndustry.BuildingProducts,
            "09" => SubIndustry.PaperProducts,
            "10" => SubIndustry.Steel,
            "11" => SubIndustry.TiresAndRubber,
            "12" => SubIndustry.AutomobileManufacturers,
            "14" => SubIndustry.ConstructionAndEngineering,
            "15" => SubIndustry.MarineTransportation,
            "16" => SubIndustry.Restaurants,
            "17" => SubIndustry.DiversifiedFinancialServices,
            "18" => SubIndustry.BroadlineRetail,
            "21" => SubIndustry.CommodityChemicals,
            "22" => SubIndustry.Biotechnology,
            "23" => SubIndustry.CoalAndConsumableFuels,
            "24" => SubIndustry.Semiconductors,
            "25" => SubIndustry.TechnologyHardwareStorageAndPeripherals,
            "26" => SubIndustry.ElectronicComponents,
            "27" => SubIndustry.AlternativeCarriers,
            "28" => SubIndustry.ElectronicEquipmentAndInstruments,
            "29" => SubIndustry.TechnologyDistributors,
            "30" => SubIndustry.ITConsultingAndOtherServices,
            "31" => SubIndustry.ElectronicManufacturingServices,
            "35" => SubIndustry.RenewableElectricity,
            "36" => SubIndustry.InternetServicesAndInfrastructure,
            "37" => SubIndustry.LeisureProducts,
            "38" => SubIndustry.HousewaresAndSpecialties,
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
