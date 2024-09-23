using Deopeia.Common.Domain.Measurement;
using Deopeia.Quote.Domain.Assets;
using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Domain.Exchanges;
using Deopeia.Quote.Domain.Instruments.FuturesContracts;
using Unit = Deopeia.Common.Domain.Measurement.Unit;

namespace Deopeia.Quote.Infrastructure;

public class QuoteSeeder : DbSeeder<QuoteContext>
{
    private static readonly CultureInfo EN = CultureInfo.GetCultureInfo("en");
    private static readonly CultureInfo ZHHant = CultureInfo.GetCultureInfo("zh-Hant");

    public override async Task SeedAsync(QuoteContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        var assets = GetAssets().ToDictionary(x => x.Code);
        var exchanges = GetExchanges().ToDictionary(x => x.Id.Mic);

        context.Set<Currency>().AddRange(GetCurrencies());
        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Unit>().AddRange(GetUnits());

        context.Set<Asset>().AddRange(assets.Values);
        context.Set<Exchange>().AddRange(exchanges.Values);
        context.Set<FuturesContract>().AddRange(GetFuturesContracts(assets, exchanges));

        await context.SaveChangesAsync();
    }

    private IEnumerable<LocaleResource> GetLocaleResources()
    {
        var resourcesEN = new LocaleResource[]
        {
            FromEnum(EN, Sector.Energy, "Energy"),
            FromEnum(EN, Sector.Materials, "Materials"),
            FromEnum(EN, Sector.Industrials, "Industrials"),
            FromEnum(EN, Sector.ConsumerDiscretionary, "Consumer Discretionary"),
            FromEnum(EN, Sector.ConsumerStaples, "Consumer Staples"),
            FromEnum(EN, Sector.HealthCare, "Health Care"),
            FromEnum(EN, Sector.Financials, "Financials"),
            FromEnum(EN, Sector.InformationTechnology, "Information Technology"),
            FromEnum(EN, Sector.CommunicationServices, "Communication Services"),
            FromEnum(EN, Sector.Utilities, "Utilities"),
            FromEnum(EN, Sector.RealEstate, "Real Estate"),
            FromEnum(EN, Industry.EnergyEquipmentAndServices, "Energy Equipment & Services"),
            FromEnum(EN, Industry.OilGasAndConsumableFuels, "Oil, Gas & Consumable Fuels"),
            FromEnum(EN, Industry.Chemicals, "Chemicals"),
            FromEnum(EN, Industry.ConstructionMaterials, "Construction Materials"),
            FromEnum(EN, Industry.ContainersAndPackaging, "Containers & Packaging"),
            FromEnum(EN, Industry.MetalsAndMining, "Metals & Mining"),
            FromEnum(EN, Industry.PaperAndForestProducts, "Paper & Forest Products"),
            FromEnum(EN, Industry.AerospaceAndDefense, "Aerospace & Defense"),
            FromEnum(EN, Industry.BuildingProducts, "Building Products"),
            FromEnum(EN, Industry.ConstructionAndEngineering, "Construction & Engineering"),
            FromEnum(EN, Industry.ElectricalEquipment, "Electrical Equipment"),
            FromEnum(EN, Industry.IndustrialConglomerates, "Industrial Conglomerates"),
            FromEnum(EN, Industry.Machinery, "Machinery"),
            FromEnum(
                EN,
                Industry.TradingCompaniesAndDistributors,
                "Trading Companies & Distributors"
            ),
            FromEnum(EN, Industry.CommercialServicesAndSupplies, "Commercial Services & Supplies"),
            FromEnum(EN, Industry.ProfessionalServices, "Professional Services"),
            FromEnum(EN, Industry.AirFreightAndLogistics, "Air Freight & Logistics"),
            FromEnum(EN, Industry.PassengerAirlines, "Passenger Airlines"),
            FromEnum(EN, Industry.MarineTransportation, "Marine Transportation"),
            FromEnum(EN, Industry.GroundTransportation, "Ground Transportation"),
            FromEnum(EN, Industry.TransportationInfrastructure, "Transportation Infrastructure"),
            FromEnum(EN, Industry.AutomobileComponents, "Automobile Components"),
            FromEnum(EN, Industry.Automobiles, "Automobiles"),
            FromEnum(EN, Industry.HouseholdDurables, "Household Durables"),
            FromEnum(EN, Industry.LeisureProducts, "Leisure Products"),
            FromEnum(
                EN,
                Industry.TextilesApparelAndLuxuryGoods,
                "Textiles, Apparel & Luxury Goods"
            ),
            FromEnum(EN, Industry.HotelsRestaurantsAndLeisure, "Hotels, Restaurants & Leisure"),
            FromEnum(EN, Industry.DiversifiedConsumerServices, "Diversified Consumer Services"),
            FromEnum(EN, Industry.Distributors, "Distributors"),
            FromEnum(EN, Industry.BroadlineRetail, "Broadline Retail"),
            FromEnum(EN, Industry.SpecialtyRetail, "Specialty Retail"),
            FromEnum(
                EN,
                Industry.ConsumerStaplesDistributionAndRetail,
                "Consumer Staples Distribution & Retail"
            ),
            FromEnum(EN, Industry.Beverages, "Beverages"),
            FromEnum(EN, Industry.FoodProducts, "Food Products"),
            FromEnum(EN, Industry.Tobacco, "Tobacco"),
            FromEnum(EN, Industry.HouseholdProducts, "Household Products"),
            FromEnum(EN, Industry.PersonalCareProducts, "Personal Care Products"),
            FromEnum(
                EN,
                Industry.HealthCareEquipmentAndSupplies,
                "Health Care Equipment & Supplies"
            ),
            FromEnum(
                EN,
                Industry.HealthCareProvidersAndServices,
                "Health Care Providers & Services"
            ),
            FromEnum(EN, Industry.HealthCareTechnology, "Health Care Technology"),
            FromEnum(EN, Industry.Biotechnology, "Biotechnology"),
            FromEnum(EN, Industry.Pharmaceuticals, "Pharmaceuticals"),
            FromEnum(EN, Industry.LifeSciencesToolsAndServices, "Life Sciences Tools & Services"),
            FromEnum(EN, Industry.Banks, "Banks"),
            FromEnum(EN, Industry.FinancialServices, " Financial Services"),
            FromEnum(EN, Industry.ConsumerFinance, "Consumer Finance"),
            FromEnum(EN, Industry.CapitalMarkets, "Capital Markets"),
            FromEnum(
                EN,
                Industry.MortgageRealEstateInvestmentTrusts,
                "Mortgage Real Estate Investment Trusts (REITs)"
            ),
            FromEnum(EN, Industry.Insurance, "Insurance"),
            FromEnum(EN, Industry.ITServices, "IT Services"),
            FromEnum(EN, Industry.Software, "Software"),
            FromEnum(EN, Industry.CommunicationsEquipment, "Communications Equipment"),
            FromEnum(
                EN,
                Industry.TechnologyHardwareStorageAndPeripherals,
                "Technology Hardware, Storage & Peripherals"
            ),
            FromEnum(
                EN,
                Industry.ElectronicEquipmentInstrumentsAndComponents,
                "Electronic Equipment, Instruments & Components"
            ),
            FromEnum(
                EN,
                Industry.SemiconductorsAndSemiconductorEquipment,
                "Semiconductors & Semiconductor Equipment"
            ),
            FromEnum(
                EN,
                Industry.DiversifiedTelecommunicationServices,
                "Diversified Telecommunication Services"
            ),
            FromEnum(
                EN,
                Industry.WirelessTelecommunicationServices,
                "Wireless Telecommunication Services"
            ),
            FromEnum(EN, Industry.Media, "Media"),
            FromEnum(EN, Industry.Entertainment, "Entertainment"),
            FromEnum(EN, Industry.InteractiveMediaAndServices, "Interactive Media & Services"),
            FromEnum(EN, Industry.ElectricUtilities, "Electric Utilities"),
            FromEnum(EN, Industry.GasUtilities, "Gas Utilities"),
            FromEnum(EN, Industry.MultiUtilities, "Multi-Utilities"),
            FromEnum(EN, Industry.WaterUtilities, "Water Utilities"),
            FromEnum(
                EN,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "Independent Power and Renewable Electricity Producers"
            ),
            FromEnum(EN, Industry.DiversifiedReits, "Diversified REITs"),
            FromEnum(EN, Industry.IndustrialReits, "Industrial REITs"),
            FromEnum(EN, Industry.HotelAndResortReits, "Hotel & Resort REITs"),
            FromEnum(EN, Industry.OfficeReits, "Office REITs"),
            FromEnum(EN, Industry.HealthCareReits, "Health Care REITs"),
            FromEnum(EN, Industry.ResidentialReits, "Residential REITs"),
            FromEnum(EN, Industry.RetailReits, "Retail REITs"),
            FromEnum(EN, Industry.SpecializedReits, "Specialized REITs"),
            FromEnum(
                EN,
                Industry.RealEstateManagementAndDevelopment,
                "Real Estate Management & Development"
            ),
            FromModel(EN, "Exchange.OpeningTime", "Opening Time"),
            FromModel(EN, "Exchange.ClosingTime", "Closing Time"),
        };

        var resourcesZHHant = new LocaleResource[]
        {
            FromEnum(ZHHant, Sector.Energy, "能源"),
            FromEnum(ZHHant, Sector.Materials, "原材料"),
            FromEnum(ZHHant, Sector.Industrials, "工業"),
            FromEnum(ZHHant, Sector.ConsumerDiscretionary, "可選消費品"),
            FromEnum(ZHHant, Sector.ConsumerStaples, "日常消費品"),
            FromEnum(ZHHant, Sector.HealthCare, "醫療保健"),
            FromEnum(ZHHant, Sector.Financials, "金融"),
            FromEnum(ZHHant, Sector.InformationTechnology, "資訊科技"),
            FromEnum(ZHHant, Sector.CommunicationServices, "通訊服務"),
            FromEnum(ZHHant, Sector.Utilities, "公用事業"),
            FromEnum(ZHHant, Sector.RealEstate, "房地產"),
            FromEnum(ZHHant, Industry.EnergyEquipmentAndServices, "能源設備與服務"),
            FromEnum(ZHHant, Industry.OilGasAndConsumableFuels, "石油、天然氣和消費用燃料"),
            FromEnum(ZHHant, Industry.Chemicals, "化學製品"),
            FromEnum(ZHHant, Industry.ConstructionMaterials, "建築材料"),
            FromEnum(ZHHant, Industry.ContainersAndPackaging, "容器與包裝"),
            FromEnum(ZHHant, Industry.MetalsAndMining, "金屬與採礦"),
            FromEnum(ZHHant, Industry.PaperAndForestProducts, "紙類與林業產品"),
            FromEnum(ZHHant, Industry.AerospaceAndDefense, "航空航太與國防"),
            FromEnum(ZHHant, Industry.BuildingProducts, "建築產品"),
            FromEnum(ZHHant, Industry.ConstructionAndEngineering, "建築與工程"),
            FromEnum(ZHHant, Industry.ElectricalEquipment, "電氣設備"),
            FromEnum(ZHHant, Industry.IndustrialConglomerates, "工業集團企業"),
            FromEnum(ZHHant, Industry.Machinery, "機械製造"),
            FromEnum(ZHHant, Industry.TradingCompaniesAndDistributors, "貿易公司與經銷商"),
            FromEnum(ZHHant, Industry.CommercialServicesAndSupplies, "商業服務與商業用品"),
            FromEnum(ZHHant, Industry.ProfessionalServices, "專業服務"),
            FromEnum(ZHHant, Industry.AirFreightAndLogistics, "航空貨運與物流"),
            FromEnum(ZHHant, Industry.PassengerAirlines, "客運航空公司"),
            FromEnum(ZHHant, Industry.MarineTransportation, "海運"),
            FromEnum(ZHHant, Industry.GroundTransportation, "陸運"),
            FromEnum(ZHHant, Industry.TransportationInfrastructure, "交通基本設施"),
            FromEnum(ZHHant, Industry.AutomobileComponents, "汽車零部件"),
            FromEnum(ZHHant, Industry.Automobiles, "汽車"),
            FromEnum(ZHHant, Industry.HouseholdDurables, "家庭耐用消費品"),
            FromEnum(ZHHant, Industry.LeisureProducts, "休閒用品"),
            FromEnum(ZHHant, Industry.TextilesApparelAndLuxuryGoods, "紡織品、服裝與奢侈品"),
            FromEnum(ZHHant, Industry.HotelsRestaurantsAndLeisure, "酒店、餐廳與休閒"),
            FromEnum(ZHHant, Industry.DiversifiedConsumerServices, "多元化消費者服務"),
            FromEnum(ZHHant, Industry.Distributors, "經銷商"),
            FromEnum(ZHHant, Industry.BroadlineRetail, "多元化零售"),
            FromEnum(ZHHant, Industry.SpecialtyRetail, "專營零售"),
            FromEnum(ZHHant, Industry.ConsumerStaplesDistributionAndRetail, "日常消費品分銷與零售"),
            FromEnum(ZHHant, Industry.Beverages, "飲品"),
            FromEnum(ZHHant, Industry.FoodProducts, "食品"),
            FromEnum(ZHHant, Industry.Tobacco, "煙草"),
            FromEnum(ZHHant, Industry.HouseholdProducts, "家庭用品"),
            FromEnum(ZHHant, Industry.PersonalCareProducts, "個人護理用品"),
            FromEnum(ZHHant, Industry.HealthCareEquipmentAndSupplies, "醫療保健設備與用品"),
            FromEnum(ZHHant, Industry.HealthCareProvidersAndServices, "健康保健供應商與服務"),
            FromEnum(ZHHant, Industry.HealthCareTechnology, "醫療保健技術"),
            FromEnum(ZHHant, Industry.Biotechnology, "生物科技"),
            FromEnum(ZHHant, Industry.Pharmaceuticals, "製藥"),
            FromEnum(ZHHant, Industry.LifeSciencesToolsAndServices, "生命科學工具與服務"),
            FromEnum(ZHHant, Industry.Banks, "銀行"),
            FromEnum(ZHHant, Industry.FinancialServices, " 金融服務"),
            FromEnum(ZHHant, Industry.ConsumerFinance, "消費者金融"),
            FromEnum(ZHHant, Industry.CapitalMarkets, "資本市場"),
            FromEnum(ZHHant, Industry.MortgageRealEstateInvestmentTrusts, "按揭房地產投資信託基金"),
            FromEnum(ZHHant, Industry.Insurance, "保險"),
            FromEnum(ZHHant, Industry.ITServices, "資訊科技服務"),
            FromEnum(ZHHant, Industry.Software, "軟體"),
            FromEnum(ZHHant, Industry.CommunicationsEquipment, "通訊設備"),
            FromEnum(ZHHant, Industry.TechnologyHardwareStorageAndPeripherals, "電腦硬體、儲存及週邊設備"),
            FromEnum(ZHHant, Industry.ElectronicEquipmentInstrumentsAndComponents, "電子設備、儀器與零件"),
            FromEnum(ZHHant, Industry.SemiconductorsAndSemiconductorEquipment, "半導體產品與設備"),
            FromEnum(ZHHant, Industry.DiversifiedTelecommunicationServices, "綜合電訊服務"),
            FromEnum(ZHHant, Industry.WirelessTelecommunicationServices, "無線電訊服務"),
            FromEnum(ZHHant, Industry.Media, "媒體"),
            FromEnum(ZHHant, Industry.Entertainment, "娛樂"),
            FromEnum(ZHHant, Industry.InteractiveMediaAndServices, "互動媒體與服務"),
            FromEnum(ZHHant, Industry.ElectricUtilities, "電力公用事業"),
            FromEnum(ZHHant, Industry.GasUtilities, "燃氣公用事業"),
            FromEnum(ZHHant, Industry.MultiUtilities, "複合型公用事業"),
            FromEnum(ZHHant, Industry.WaterUtilities, "水務公用事業"),
            FromEnum(
                ZHHant,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "獨立電力及可再生電力生產商"
            ),
            FromEnum(ZHHant, Industry.DiversifiedReits, "多元化房地產投資信託基金"),
            FromEnum(ZHHant, Industry.IndustrialReits, "工業房地產投資信託基金"),
            FromEnum(ZHHant, Industry.HotelAndResortReits, "酒店及度假村房地產投資信託基金"),
            FromEnum(ZHHant, Industry.OfficeReits, "辦公室房地產投資信託基金"),
            FromEnum(ZHHant, Industry.HealthCareReits, "醫療保健房地產投資信託基金"),
            FromEnum(ZHHant, Industry.ResidentialReits, "住宅房地產投資信託基金"),
            FromEnum(ZHHant, Industry.RetailReits, "零售業房地產投資信託基金"),
            FromEnum(ZHHant, Industry.SpecializedReits, "特種房地產投資信託基金"),
            FromEnum(ZHHant, Industry.RealEstateManagementAndDevelopment, "房地產管理與開發"),
            FromModel(ZHHant, "Exchange.OpeningTime", "開盤時間"),
            FromModel(ZHHant, "Exchange.ClosingTime", "收盤時間"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

        return results;
    }

    private IEnumerable<Asset> GetAssets()
    {
        var results = new Asset[]
        {
            new("XAU", "Gold", null),
            new("XAG", "Silver", null),
            new("XPD", "Palladium", null),
            new("XPT", "Platinum", null),
        };

        results[0].UpdateName("黃金", ZHHant);
        results[1].UpdateName("白銀", ZHHant);
        results[2].UpdateName("鈀金", ZHHant);
        results[3].UpdateName("鉑金", ZHHant);

        return results;
    }

    private IEnumerable<Exchange> GetExchanges()
    {
        var taipei = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
        var china = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
        var gmt = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
        var tokyo = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
        var eastern = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        var results = new[]
        {
            new Exchange("XTAI", "Taiwan Stock Exchange", "TWSE", taipei),
            new Exchange("XTAF", "Taiwan Futures Exchange", "TAIFEX", taipei),
            new Exchange("XZCE", "Zhengzhou Commodity Exchange", "ZCE", china),
            new Exchange("XDCE", "Dalian Commodity Exchange", "DCE", china),
            new Exchange("XSGE", "Shanghai Futures Exchange", "SHFE", china),
            new Exchange("CCFX", "China Financial Futures Exchange", "CFFEX", china),
            new Exchange("XHKF", "Hong Kong Futures Exchange", "HKFE", china),
            new Exchange("XLME", "London Metal Exchange", "LME", gmt),
            new Exchange("XTKT", "Tokyo Commodity Exchange", "TOCOM", tokyo),
            new Exchange("XNYM", "New York Mercantile Exchange", "NYMEX", eastern),
            new Exchange("XCEC", "Commodities Exchange Center", "COMEX", eastern),
        };

        results[0].UpdateName("臺灣證券交易所", ZHHant);
        results[1].UpdateName("臺灣期貨交易所", ZHHant);
        results[2].UpdateName("鄭州商品交易所", ZHHant);
        results[3].UpdateName("大連商品交易所", ZHHant);
        results[4].UpdateName("上海期貨交易所", ZHHant);
        results[5].UpdateName("中國金融期貨交易所", ZHHant);
        results[6].UpdateName("香港期貨交易所", ZHHant);
        results[7].UpdateName("倫敦金屬交易所", ZHHant);
        results[8].UpdateName("東京商品交易所", ZHHant);
        results[9].UpdateName("紐約商業交易所", ZHHant);
        results[10].UpdateName("紐約商品交易所", ZHHant);

        return results;
    }

    private IEnumerable<FuturesContract> GetFuturesContracts(
        Dictionary<string, Asset> assets,
        Dictionary<string, Exchange> exchanges
    )
    {
        var comex = exchanges["XCEC"].Id;

        var cny = new CurrencyCode("CNY");
        var jpy = new CurrencyCode("JPY");
        var usd = new CurrencyCode("USD");

        var troyOunce = new UnitCode("APZ");
        var gram = new UnitCode("GRM");
        var kilogram = new UnitCode("KGM");

        var gold = assets["XAU"].Id;
        var goldContracts = new[]
        {
            new FuturesContract(
                exchanges["XTAF"].Id,
                "GDF",
                "TAIFEX Gold Futures",
                usd,
                gold,
                new ContractSize(10, troyOunce),
                0.1M
            ),
            new FuturesContract(
                exchanges["XSGE"].Id,
                "CU",
                "Gold",
                cny,
                gold,
                new ContractSize(1000, gram),
                0.02M
            ),
            new FuturesContract(
                exchanges["XHKF"].Id,
                "GDU",
                "USD Gold Futures",
                usd,
                gold,
                new ContractSize(1000, gram),
                0.01M
            ),
            new FuturesContract(
                exchanges["XTKT"].Id,
                "JAU",
                "Gold Standard Futures",
                jpy,
                gold,
                new ContractSize(1, kilogram),
                1M
            ),
            new FuturesContract(
                exchanges["XTKT"].Id,
                "JAM",
                "Gold Mini Futures",
                jpy,
                gold,
                new ContractSize(100, gram),
                0.5M
            ),
            new FuturesContract(
                comex,
                "GC",
                "Gold Futures",
                usd,
                gold,
                new ContractSize(100, troyOunce),
                0.1M
            ),
            new FuturesContract(
                comex,
                "QO",
                "E-mini Gold Futures",
                usd,
                gold,
                new ContractSize(50, troyOunce),
                0.25M
            ),
            new FuturesContract(
                comex,
                "MGC",
                "Micro Gold Futures",
                usd,
                gold,
                new ContractSize(10, troyOunce),
                0.1M
            ),
        };

        goldContracts[0].UpdateName("黃金期貨", ZHHant);
        goldContracts[1].UpdateName("黃金", ZHHant);
        goldContracts[2].UpdateName("美元黃金", ZHHant);
        goldContracts[5].UpdateName("黃金期貨", ZHHant);
        goldContracts[5].UpdateName("黃金期貨", ZHHant);
        goldContracts[6].UpdateName("E-迷你黃金期貨", ZHHant);
        goldContracts[7].UpdateName("微型黃金期貨", ZHHant);

        var silver = assets["XAG"].Id;
        var silverContracts = new[]
        {
            new FuturesContract(
                exchanges["XSGE"].Id,
                "AG",
                "Silver",
                cny,
                silver,
                new ContractSize(15, kilogram),
                1M
            ),
            new FuturesContract(
                exchanges["XHKF"].Id,
                "SIU",
                "USD Silver Futures",
                usd,
                silver,
                new ContractSize(30, kilogram),
                0.05M
            ),
            new FuturesContract(
                exchanges["XTKT"].Id,
                "JSV",
                "Silver Futures",
                jpy,
                silver,
                new ContractSize(30, kilogram),
                1M
            ),
            new FuturesContract(
                comex,
                "SI",
                "Silver Futures",
                usd,
                silver,
                new ContractSize(5000, troyOunce),
                0.005M
            ),
            new FuturesContract(
                comex,
                "QI",
                "E-mini Silver Futures",
                usd,
                silver,
                new ContractSize(2500, troyOunce),
                0.0125M
            ),
            new FuturesContract(
                comex,
                "SIL",
                "Micro Silver Futures",
                usd,
                silver,
                new ContractSize(1000, troyOunce),
                0.005M
            ),
        };

        silverContracts[0].UpdateName("白銀", ZHHant);
        silverContracts[1].UpdateName("美元白銀", ZHHant);
        silverContracts[3].UpdateName("白銀期貨", ZHHant);
        silverContracts[4].UpdateName("E-迷你白銀期貨", ZHHant);
        silverContracts[5].UpdateName("微型白銀期貨", ZHHant);

        return goldContracts.Concat(silverContracts);
    }
}
