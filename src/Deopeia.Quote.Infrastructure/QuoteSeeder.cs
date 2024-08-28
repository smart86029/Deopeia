using Deopeia.Common.Localization;
using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure;

public class QuoteSeeder : DbSeeder<QuoteContext>
{
    public override async Task SeedAsync(QuoteContext context)
    {
        if (context.Set<LocaleResource>().Any())
        {
            return;
        }

        context.Set<LocaleResource>().AddRange(GetLocaleResources());
        context.Set<Exchange>().AddRange(GetExchanges());

        await context.SaveChangesAsync();
    }

    private IEnumerable<LocaleResource> GetLocaleResources()
    {
        var enUS = CultureInfo.GetCultureInfo("en-US");
        var resourcesENUS = new LocaleResource[]
        {
            FromEnum(enUS, Sector.Energy, "Energy"),
            FromEnum(enUS, Sector.Materials, "Materials"),
            FromEnum(enUS, Sector.Industrials, "Industrials"),
            FromEnum(enUS, Sector.ConsumerDiscretionary, "Consumer Discretionary"),
            FromEnum(enUS, Sector.ConsumerStaples, "Consumer Staples"),
            FromEnum(enUS, Sector.HealthCare, "Health Care"),
            FromEnum(enUS, Sector.Financials, "Financials"),
            FromEnum(enUS, Sector.InformationTechnology, "Information Technology"),
            FromEnum(enUS, Sector.CommunicationServices, "Communication Services"),
            FromEnum(enUS, Sector.Utilities, "Utilities"),
            FromEnum(enUS, Sector.RealEstate, "Real Estate"),
            FromEnum(enUS, Industry.EnergyEquipmentAndServices, "Energy Equipment & Services"),
            FromEnum(enUS, Industry.OilGasAndConsumableFuels, "Oil, Gas & Consumable Fuels"),
            FromEnum(enUS, Industry.Chemicals, "Chemicals"),
            FromEnum(enUS, Industry.ConstructionMaterials, "Construction Materials"),
            FromEnum(enUS, Industry.ContainersAndPackaging, "Containers & Packaging"),
            FromEnum(enUS, Industry.MetalsAndMining, "Metals & Mining"),
            FromEnum(enUS, Industry.PaperAndForestProducts, "Paper & Forest Products"),
            FromEnum(enUS, Industry.AerospaceAndDefense, "Aerospace & Defense"),
            FromEnum(enUS, Industry.BuildingProducts, "Building Products"),
            FromEnum(enUS, Industry.ConstructionAndEngineering, "Construction & Engineering"),
            FromEnum(enUS, Industry.ElectricalEquipment, "Electrical Equipment"),
            FromEnum(enUS, Industry.IndustrialConglomerates, "Industrial Conglomerates"),
            FromEnum(enUS, Industry.Machinery, "Machinery"),
            FromEnum(
                enUS,
                Industry.TradingCompaniesAndDistributors,
                "Trading Companies & Distributors"
            ),
            FromEnum(
                enUS,
                Industry.CommercialServicesAndSupplies,
                "Commercial Services & Supplies"
            ),
            FromEnum(enUS, Industry.ProfessionalServices, "Professional Services"),
            FromEnum(enUS, Industry.AirFreightAndLogistics, "Air Freight & Logistics"),
            FromEnum(enUS, Industry.PassengerAirlines, "Passenger Airlines"),
            FromEnum(enUS, Industry.MarineTransportation, "Marine Transportation"),
            FromEnum(enUS, Industry.GroundTransportation, "Ground Transportation"),
            FromEnum(enUS, Industry.TransportationInfrastructure, "Transportation Infrastructure"),
            FromEnum(enUS, Industry.AutomobileComponents, "Automobile Components"),
            FromEnum(enUS, Industry.Automobiles, "Automobiles"),
            FromEnum(enUS, Industry.HouseholdDurables, "Household Durables"),
            FromEnum(enUS, Industry.LeisureProducts, "Leisure Products"),
            FromEnum(
                enUS,
                Industry.TextilesApparelAndLuxuryGoods,
                "Textiles, Apparel & Luxury Goods"
            ),
            FromEnum(enUS, Industry.HotelsRestaurantsAndLeisure, "Hotels, Restaurants & Leisure"),
            FromEnum(enUS, Industry.DiversifiedConsumerServices, "Diversified Consumer Services"),
            FromEnum(enUS, Industry.Distributors, "Distributors"),
            FromEnum(enUS, Industry.BroadlineRetail, "Broadline Retail"),
            FromEnum(enUS, Industry.SpecialtyRetail, "Specialty Retail"),
            FromEnum(
                enUS,
                Industry.ConsumerStaplesDistributionAndRetail,
                "Consumer Staples Distribution & Retail"
            ),
            FromEnum(enUS, Industry.Beverages, "Beverages"),
            FromEnum(enUS, Industry.FoodProducts, "Food Products"),
            FromEnum(enUS, Industry.Tobacco, "Tobacco"),
            FromEnum(enUS, Industry.HouseholdProducts, "Household Products"),
            FromEnum(enUS, Industry.PersonalCareProducts, "Personal Care Products"),
            FromEnum(
                enUS,
                Industry.HealthCareEquipmentAndSupplies,
                "Health Care Equipment & Supplies"
            ),
            FromEnum(
                enUS,
                Industry.HealthCareProvidersAndServices,
                "Health Care Providers & Services"
            ),
            FromEnum(enUS, Industry.HealthCareTechnology, "Health Care Technology"),
            FromEnum(enUS, Industry.Biotechnology, "Biotechnology"),
            FromEnum(enUS, Industry.Pharmaceuticals, "Pharmaceuticals"),
            FromEnum(enUS, Industry.LifeSciencesToolsAndServices, "Life Sciences Tools & Services"),
            FromEnum(enUS, Industry.Banks, "Banks"),
            FromEnum(enUS, Industry.FinancialServices, " Financial Services"),
            FromEnum(enUS, Industry.ConsumerFinance, "Consumer Finance"),
            FromEnum(enUS, Industry.CapitalMarkets, "Capital Markets"),
            FromEnum(
                enUS,
                Industry.MortgageRealEstateInvestmentTrusts,
                "Mortgage Real Estate Investment Trusts (REITs)"
            ),
            FromEnum(enUS, Industry.Insurance, "Insurance"),
            FromEnum(enUS, Industry.ITServices, "IT Services"),
            FromEnum(enUS, Industry.Software, "Software"),
            FromEnum(enUS, Industry.CommunicationsEquipment, "Communications Equipment"),
            FromEnum(
                enUS,
                Industry.TechnologyHardwareStorageAndPeripherals,
                "Technology Hardware, Storage & Peripherals"
            ),
            FromEnum(
                enUS,
                Industry.ElectronicEquipmentInstrumentsAndComponents,
                "Electronic Equipment, Instruments & Components"
            ),
            FromEnum(
                enUS,
                Industry.SemiconductorsAndSemiconductorEquipment,
                "Semiconductors & Semiconductor Equipment"
            ),
            FromEnum(
                enUS,
                Industry.DiversifiedTelecommunicationServices,
                "Diversified Telecommunication Services"
            ),
            FromEnum(
                enUS,
                Industry.WirelessTelecommunicationServices,
                "Wireless Telecommunication Services"
            ),
            FromEnum(enUS, Industry.Media, "Media"),
            FromEnum(enUS, Industry.Entertainment, "Entertainment"),
            FromEnum(enUS, Industry.InteractiveMediaAndServices, "Interactive Media & Services"),
            FromEnum(enUS, Industry.ElectricUtilities, "Electric Utilities"),
            FromEnum(enUS, Industry.GasUtilities, "Gas Utilities"),
            FromEnum(enUS, Industry.MultiUtilities, "Multi-Utilities"),
            FromEnum(enUS, Industry.WaterUtilities, "Water Utilities"),
            FromEnum(
                enUS,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "Independent Power and Renewable Electricity Producers"
            ),
            FromEnum(enUS, Industry.DiversifiedReits, "Diversified REITs"),
            FromEnum(enUS, Industry.IndustrialReits, "Industrial REITs"),
            FromEnum(enUS, Industry.HotelAndResortReits, "Hotel & Resort REITs"),
            FromEnum(enUS, Industry.OfficeReits, "Office REITs"),
            FromEnum(enUS, Industry.HealthCareReits, "Health Care REITs"),
            FromEnum(enUS, Industry.ResidentialReits, "Residential REITs"),
            FromEnum(enUS, Industry.RetailReits, "Retail REITs"),
            FromEnum(enUS, Industry.SpecializedReits, "Specialized REITs"),
            FromEnum(
                enUS,
                Industry.RealEstateManagementAndDevelopment,
                "Real Estate Management & Development"
            ),
            FromModel(enUS, "Exchange.OpeningTime", "Opening Time"),
            FromModel(enUS, "Exchange.ClosingTime", "Closing Time"),
        };

        var zhTW = CultureInfo.GetCultureInfo("zh-TW");
        var resourcesZHTW = new LocaleResource[]
        {
            FromEnum(zhTW, Sector.Energy, "能源"),
            FromEnum(zhTW, Sector.Materials, "原材料"),
            FromEnum(zhTW, Sector.Industrials, "工業"),
            FromEnum(zhTW, Sector.ConsumerDiscretionary, "可選消費品"),
            FromEnum(zhTW, Sector.ConsumerStaples, "日常消費品"),
            FromEnum(zhTW, Sector.HealthCare, "醫療保健"),
            FromEnum(zhTW, Sector.Financials, "金融"),
            FromEnum(zhTW, Sector.InformationTechnology, "資訊科技"),
            FromEnum(zhTW, Sector.CommunicationServices, "通訊服務"),
            FromEnum(zhTW, Sector.Utilities, "公用事業"),
            FromEnum(zhTW, Sector.RealEstate, "房地產"),
            FromEnum(zhTW, Industry.EnergyEquipmentAndServices, "能源設備與服務"),
            FromEnum(zhTW, Industry.OilGasAndConsumableFuels, "石油、天然氣和消費用燃料"),
            FromEnum(zhTW, Industry.Chemicals, "化學製品"),
            FromEnum(zhTW, Industry.ConstructionMaterials, "建築材料"),
            FromEnum(zhTW, Industry.ContainersAndPackaging, "容器與包裝"),
            FromEnum(zhTW, Industry.MetalsAndMining, "金屬與採礦"),
            FromEnum(zhTW, Industry.PaperAndForestProducts, "紙類與林業產品"),
            FromEnum(zhTW, Industry.AerospaceAndDefense, "航空航太與國防"),
            FromEnum(zhTW, Industry.BuildingProducts, "建築產品"),
            FromEnum(zhTW, Industry.ConstructionAndEngineering, "建築與工程"),
            FromEnum(zhTW, Industry.ElectricalEquipment, "電氣設備"),
            FromEnum(zhTW, Industry.IndustrialConglomerates, "工業集團企業"),
            FromEnum(zhTW, Industry.Machinery, "機械製造"),
            FromEnum(zhTW, Industry.TradingCompaniesAndDistributors, "貿易公司與經銷商"),
            FromEnum(zhTW, Industry.CommercialServicesAndSupplies, "商業服務與商業用品"),
            FromEnum(zhTW, Industry.ProfessionalServices, "專業服務"),
            FromEnum(zhTW, Industry.AirFreightAndLogistics, "航空貨運與物流"),
            FromEnum(zhTW, Industry.PassengerAirlines, "客運航空公司"),
            FromEnum(zhTW, Industry.MarineTransportation, "海運"),
            FromEnum(zhTW, Industry.GroundTransportation, "陸運"),
            FromEnum(zhTW, Industry.TransportationInfrastructure, "交通基本設施"),
            FromEnum(zhTW, Industry.AutomobileComponents, "汽車零部件"),
            FromEnum(zhTW, Industry.Automobiles, "汽車"),
            FromEnum(zhTW, Industry.HouseholdDurables, "家庭耐用消費品"),
            FromEnum(zhTW, Industry.LeisureProducts, "休閒用品"),
            FromEnum(zhTW, Industry.TextilesApparelAndLuxuryGoods, "紡織品、服裝與奢侈品"),
            FromEnum(zhTW, Industry.HotelsRestaurantsAndLeisure, "酒店、餐廳與休閒"),
            FromEnum(zhTW, Industry.DiversifiedConsumerServices, "多元化消費者服務"),
            FromEnum(zhTW, Industry.Distributors, "經銷商"),
            FromEnum(zhTW, Industry.BroadlineRetail, "多元化零售"),
            FromEnum(zhTW, Industry.SpecialtyRetail, "專營零售"),
            FromEnum(zhTW, Industry.ConsumerStaplesDistributionAndRetail, "日常消費品分銷與零售"),
            FromEnum(zhTW, Industry.Beverages, "飲品"),
            FromEnum(zhTW, Industry.FoodProducts, "食品"),
            FromEnum(zhTW, Industry.Tobacco, "煙草"),
            FromEnum(zhTW, Industry.HouseholdProducts, "家庭用品"),
            FromEnum(zhTW, Industry.PersonalCareProducts, "個人護理用品"),
            FromEnum(zhTW, Industry.HealthCareEquipmentAndSupplies, "醫療保健設備與用品"),
            FromEnum(zhTW, Industry.HealthCareProvidersAndServices, "健康保健供應商與服務"),
            FromEnum(zhTW, Industry.HealthCareTechnology, "醫療保健技術"),
            FromEnum(zhTW, Industry.Biotechnology, "生物科技"),
            FromEnum(zhTW, Industry.Pharmaceuticals, "製藥"),
            FromEnum(zhTW, Industry.LifeSciencesToolsAndServices, "生命科學工具與服務"),
            FromEnum(zhTW, Industry.Banks, "銀行"),
            FromEnum(zhTW, Industry.FinancialServices, " 金融服務"),
            FromEnum(zhTW, Industry.ConsumerFinance, "消費者金融"),
            FromEnum(zhTW, Industry.CapitalMarkets, "資本市場"),
            FromEnum(zhTW, Industry.MortgageRealEstateInvestmentTrusts, "按揭房地產投資信託基金"),
            FromEnum(zhTW, Industry.Insurance, "保險"),
            FromEnum(zhTW, Industry.ITServices, "資訊科技服務"),
            FromEnum(zhTW, Industry.Software, "軟體"),
            FromEnum(zhTW, Industry.CommunicationsEquipment, "通訊設備"),
            FromEnum(zhTW, Industry.TechnologyHardwareStorageAndPeripherals, "電腦硬體、儲存及週邊設備"),
            FromEnum(zhTW, Industry.ElectronicEquipmentInstrumentsAndComponents, "電子設備、儀器與零件"),
            FromEnum(zhTW, Industry.SemiconductorsAndSemiconductorEquipment, "半導體產品與設備"),
            FromEnum(zhTW, Industry.DiversifiedTelecommunicationServices, "綜合電訊服務"),
            FromEnum(zhTW, Industry.WirelessTelecommunicationServices, "無線電訊服務"),
            FromEnum(zhTW, Industry.Media, "媒體"),
            FromEnum(zhTW, Industry.Entertainment, "娛樂"),
            FromEnum(zhTW, Industry.InteractiveMediaAndServices, "互動媒體與服務"),
            FromEnum(zhTW, Industry.ElectricUtilities, "電力公用事業"),
            FromEnum(zhTW, Industry.GasUtilities, "燃氣公用事業"),
            FromEnum(zhTW, Industry.MultiUtilities, "複合型公用事業"),
            FromEnum(zhTW, Industry.WaterUtilities, "水務公用事業"),
            FromEnum(
                zhTW,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "獨立電力及可再生電力生產商"
            ),
            FromEnum(zhTW, Industry.DiversifiedReits, "多元化房地產投資信託基金"),
            FromEnum(zhTW, Industry.IndustrialReits, "工業房地產投資信託基金"),
            FromEnum(zhTW, Industry.HotelAndResortReits, "酒店及度假村房地產投資信託基金"),
            FromEnum(zhTW, Industry.OfficeReits, "辦公室房地產投資信託基金"),
            FromEnum(zhTW, Industry.HealthCareReits, "醫療保健房地產投資信託基金"),
            FromEnum(zhTW, Industry.ResidentialReits, "住宅房地產投資信託基金"),
            FromEnum(zhTW, Industry.RetailReits, "零售業房地產投資信託基金"),
            FromEnum(zhTW, Industry.SpecializedReits, "特種房地產投資信託基金"),
            FromEnum(zhTW, Industry.RealEstateManagementAndDevelopment, "房地產管理與開發"),
            FromModel(zhTW, "Exchange.OpeningTime", "開盤時間"),
            FromModel(zhTW, "Exchange.ClosingTime", "收盤時間"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesENUS).Concat(resourcesZHTW);

        return results;
    }

    private IEnumerable<Exchange> GetExchanges()
    {
        var exchangeXtai = new Exchange(
            "XTAI",
            "Taiwan Stock Exchange",
            TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"),
            new TimeOnly(9, 0),
            new TimeOnly(13, 30)
        );
        exchangeXtai.UpdateName("臺灣證券交易所", CultureInfo.GetCultureInfo("zh-TW"));

        var results = new[] { exchangeXtai, };

        return results;
    }
}
