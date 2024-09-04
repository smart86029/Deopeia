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
        var en = CultureInfo.GetCultureInfo("en");
        var resourcesEN = new LocaleResource[]
        {
            FromEnum(en, Sector.Energy, "Energy"),
            FromEnum(en, Sector.Materials, "Materials"),
            FromEnum(en, Sector.Industrials, "Industrials"),
            FromEnum(en, Sector.ConsumerDiscretionary, "Consumer Discretionary"),
            FromEnum(en, Sector.ConsumerStaples, "Consumer Staples"),
            FromEnum(en, Sector.HealthCare, "Health Care"),
            FromEnum(en, Sector.Financials, "Financials"),
            FromEnum(en, Sector.InformationTechnology, "Information Technology"),
            FromEnum(en, Sector.CommunicationServices, "Communication Services"),
            FromEnum(en, Sector.Utilities, "Utilities"),
            FromEnum(en, Sector.RealEstate, "Real Estate"),
            FromEnum(en, Industry.EnergyEquipmentAndServices, "Energy Equipment & Services"),
            FromEnum(en, Industry.OilGasAndConsumableFuels, "Oil, Gas & Consumable Fuels"),
            FromEnum(en, Industry.Chemicals, "Chemicals"),
            FromEnum(en, Industry.ConstructionMaterials, "Construction Materials"),
            FromEnum(en, Industry.ContainersAndPackaging, "Containers & Packaging"),
            FromEnum(en, Industry.MetalsAndMining, "Metals & Mining"),
            FromEnum(en, Industry.PaperAndForestProducts, "Paper & Forest Products"),
            FromEnum(en, Industry.AerospaceAndDefense, "Aerospace & Defense"),
            FromEnum(en, Industry.BuildingProducts, "Building Products"),
            FromEnum(en, Industry.ConstructionAndEngineering, "Construction & Engineering"),
            FromEnum(en, Industry.ElectricalEquipment, "Electrical Equipment"),
            FromEnum(en, Industry.IndustrialConglomerates, "Industrial Conglomerates"),
            FromEnum(en, Industry.Machinery, "Machinery"),
            FromEnum(
                en,
                Industry.TradingCompaniesAndDistributors,
                "Trading Companies & Distributors"
            ),
            FromEnum(en, Industry.CommercialServicesAndSupplies, "Commercial Services & Supplies"),
            FromEnum(en, Industry.ProfessionalServices, "Professional Services"),
            FromEnum(en, Industry.AirFreightAndLogistics, "Air Freight & Logistics"),
            FromEnum(en, Industry.PassengerAirlines, "Passenger Airlines"),
            FromEnum(en, Industry.MarineTransportation, "Marine Transportation"),
            FromEnum(en, Industry.GroundTransportation, "Ground Transportation"),
            FromEnum(en, Industry.TransportationInfrastructure, "Transportation Infrastructure"),
            FromEnum(en, Industry.AutomobileComponents, "Automobile Components"),
            FromEnum(en, Industry.Automobiles, "Automobiles"),
            FromEnum(en, Industry.HouseholdDurables, "Household Durables"),
            FromEnum(en, Industry.LeisureProducts, "Leisure Products"),
            FromEnum(
                en,
                Industry.TextilesApparelAndLuxuryGoods,
                "Textiles, Apparel & Luxury Goods"
            ),
            FromEnum(en, Industry.HotelsRestaurantsAndLeisure, "Hotels, Restaurants & Leisure"),
            FromEnum(en, Industry.DiversifiedConsumerServices, "Diversified Consumer Services"),
            FromEnum(en, Industry.Distributors, "Distributors"),
            FromEnum(en, Industry.BroadlineRetail, "Broadline Retail"),
            FromEnum(en, Industry.SpecialtyRetail, "Specialty Retail"),
            FromEnum(
                en,
                Industry.ConsumerStaplesDistributionAndRetail,
                "Consumer Staples Distribution & Retail"
            ),
            FromEnum(en, Industry.Beverages, "Beverages"),
            FromEnum(en, Industry.FoodProducts, "Food Products"),
            FromEnum(en, Industry.Tobacco, "Tobacco"),
            FromEnum(en, Industry.HouseholdProducts, "Household Products"),
            FromEnum(en, Industry.PersonalCareProducts, "Personal Care Products"),
            FromEnum(
                en,
                Industry.HealthCareEquipmentAndSupplies,
                "Health Care Equipment & Supplies"
            ),
            FromEnum(
                en,
                Industry.HealthCareProvidersAndServices,
                "Health Care Providers & Services"
            ),
            FromEnum(en, Industry.HealthCareTechnology, "Health Care Technology"),
            FromEnum(en, Industry.Biotechnology, "Biotechnology"),
            FromEnum(en, Industry.Pharmaceuticals, "Pharmaceuticals"),
            FromEnum(en, Industry.LifeSciencesToolsAndServices, "Life Sciences Tools & Services"),
            FromEnum(en, Industry.Banks, "Banks"),
            FromEnum(en, Industry.FinancialServices, " Financial Services"),
            FromEnum(en, Industry.ConsumerFinance, "Consumer Finance"),
            FromEnum(en, Industry.CapitalMarkets, "Capital Markets"),
            FromEnum(
                en,
                Industry.MortgageRealEstateInvestmentTrusts,
                "Mortgage Real Estate Investment Trusts (REITs)"
            ),
            FromEnum(en, Industry.Insurance, "Insurance"),
            FromEnum(en, Industry.ITServices, "IT Services"),
            FromEnum(en, Industry.Software, "Software"),
            FromEnum(en, Industry.CommunicationsEquipment, "Communications Equipment"),
            FromEnum(
                en,
                Industry.TechnologyHardwareStorageAndPeripherals,
                "Technology Hardware, Storage & Peripherals"
            ),
            FromEnum(
                en,
                Industry.ElectronicEquipmentInstrumentsAndComponents,
                "Electronic Equipment, Instruments & Components"
            ),
            FromEnum(
                en,
                Industry.SemiconductorsAndSemiconductorEquipment,
                "Semiconductors & Semiconductor Equipment"
            ),
            FromEnum(
                en,
                Industry.DiversifiedTelecommunicationServices,
                "Diversified Telecommunication Services"
            ),
            FromEnum(
                en,
                Industry.WirelessTelecommunicationServices,
                "Wireless Telecommunication Services"
            ),
            FromEnum(en, Industry.Media, "Media"),
            FromEnum(en, Industry.Entertainment, "Entertainment"),
            FromEnum(en, Industry.InteractiveMediaAndServices, "Interactive Media & Services"),
            FromEnum(en, Industry.ElectricUtilities, "Electric Utilities"),
            FromEnum(en, Industry.GasUtilities, "Gas Utilities"),
            FromEnum(en, Industry.MultiUtilities, "Multi-Utilities"),
            FromEnum(en, Industry.WaterUtilities, "Water Utilities"),
            FromEnum(
                en,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "Independent Power and Renewable Electricity Producers"
            ),
            FromEnum(en, Industry.DiversifiedReits, "Diversified REITs"),
            FromEnum(en, Industry.IndustrialReits, "Industrial REITs"),
            FromEnum(en, Industry.HotelAndResortReits, "Hotel & Resort REITs"),
            FromEnum(en, Industry.OfficeReits, "Office REITs"),
            FromEnum(en, Industry.HealthCareReits, "Health Care REITs"),
            FromEnum(en, Industry.ResidentialReits, "Residential REITs"),
            FromEnum(en, Industry.RetailReits, "Retail REITs"),
            FromEnum(en, Industry.SpecializedReits, "Specialized REITs"),
            FromEnum(
                en,
                Industry.RealEstateManagementAndDevelopment,
                "Real Estate Management & Development"
            ),
            FromModel(en, "Exchange.OpeningTime", "Opening Time"),
            FromModel(en, "Exchange.ClosingTime", "Closing Time"),
        };

        var zhHant = CultureInfo.GetCultureInfo("zh-Hant");
        var resourcesZHHant = new LocaleResource[]
        {
            FromEnum(zhHant, Sector.Energy, "能源"),
            FromEnum(zhHant, Sector.Materials, "原材料"),
            FromEnum(zhHant, Sector.Industrials, "工業"),
            FromEnum(zhHant, Sector.ConsumerDiscretionary, "可選消費品"),
            FromEnum(zhHant, Sector.ConsumerStaples, "日常消費品"),
            FromEnum(zhHant, Sector.HealthCare, "醫療保健"),
            FromEnum(zhHant, Sector.Financials, "金融"),
            FromEnum(zhHant, Sector.InformationTechnology, "資訊科技"),
            FromEnum(zhHant, Sector.CommunicationServices, "通訊服務"),
            FromEnum(zhHant, Sector.Utilities, "公用事業"),
            FromEnum(zhHant, Sector.RealEstate, "房地產"),
            FromEnum(zhHant, Industry.EnergyEquipmentAndServices, "能源設備與服務"),
            FromEnum(zhHant, Industry.OilGasAndConsumableFuels, "石油、天然氣和消費用燃料"),
            FromEnum(zhHant, Industry.Chemicals, "化學製品"),
            FromEnum(zhHant, Industry.ConstructionMaterials, "建築材料"),
            FromEnum(zhHant, Industry.ContainersAndPackaging, "容器與包裝"),
            FromEnum(zhHant, Industry.MetalsAndMining, "金屬與採礦"),
            FromEnum(zhHant, Industry.PaperAndForestProducts, "紙類與林業產品"),
            FromEnum(zhHant, Industry.AerospaceAndDefense, "航空航太與國防"),
            FromEnum(zhHant, Industry.BuildingProducts, "建築產品"),
            FromEnum(zhHant, Industry.ConstructionAndEngineering, "建築與工程"),
            FromEnum(zhHant, Industry.ElectricalEquipment, "電氣設備"),
            FromEnum(zhHant, Industry.IndustrialConglomerates, "工業集團企業"),
            FromEnum(zhHant, Industry.Machinery, "機械製造"),
            FromEnum(zhHant, Industry.TradingCompaniesAndDistributors, "貿易公司與經銷商"),
            FromEnum(zhHant, Industry.CommercialServicesAndSupplies, "商業服務與商業用品"),
            FromEnum(zhHant, Industry.ProfessionalServices, "專業服務"),
            FromEnum(zhHant, Industry.AirFreightAndLogistics, "航空貨運與物流"),
            FromEnum(zhHant, Industry.PassengerAirlines, "客運航空公司"),
            FromEnum(zhHant, Industry.MarineTransportation, "海運"),
            FromEnum(zhHant, Industry.GroundTransportation, "陸運"),
            FromEnum(zhHant, Industry.TransportationInfrastructure, "交通基本設施"),
            FromEnum(zhHant, Industry.AutomobileComponents, "汽車零部件"),
            FromEnum(zhHant, Industry.Automobiles, "汽車"),
            FromEnum(zhHant, Industry.HouseholdDurables, "家庭耐用消費品"),
            FromEnum(zhHant, Industry.LeisureProducts, "休閒用品"),
            FromEnum(zhHant, Industry.TextilesApparelAndLuxuryGoods, "紡織品、服裝與奢侈品"),
            FromEnum(zhHant, Industry.HotelsRestaurantsAndLeisure, "酒店、餐廳與休閒"),
            FromEnum(zhHant, Industry.DiversifiedConsumerServices, "多元化消費者服務"),
            FromEnum(zhHant, Industry.Distributors, "經銷商"),
            FromEnum(zhHant, Industry.BroadlineRetail, "多元化零售"),
            FromEnum(zhHant, Industry.SpecialtyRetail, "專營零售"),
            FromEnum(zhHant, Industry.ConsumerStaplesDistributionAndRetail, "日常消費品分銷與零售"),
            FromEnum(zhHant, Industry.Beverages, "飲品"),
            FromEnum(zhHant, Industry.FoodProducts, "食品"),
            FromEnum(zhHant, Industry.Tobacco, "煙草"),
            FromEnum(zhHant, Industry.HouseholdProducts, "家庭用品"),
            FromEnum(zhHant, Industry.PersonalCareProducts, "個人護理用品"),
            FromEnum(zhHant, Industry.HealthCareEquipmentAndSupplies, "醫療保健設備與用品"),
            FromEnum(zhHant, Industry.HealthCareProvidersAndServices, "健康保健供應商與服務"),
            FromEnum(zhHant, Industry.HealthCareTechnology, "醫療保健技術"),
            FromEnum(zhHant, Industry.Biotechnology, "生物科技"),
            FromEnum(zhHant, Industry.Pharmaceuticals, "製藥"),
            FromEnum(zhHant, Industry.LifeSciencesToolsAndServices, "生命科學工具與服務"),
            FromEnum(zhHant, Industry.Banks, "銀行"),
            FromEnum(zhHant, Industry.FinancialServices, " 金融服務"),
            FromEnum(zhHant, Industry.ConsumerFinance, "消費者金融"),
            FromEnum(zhHant, Industry.CapitalMarkets, "資本市場"),
            FromEnum(zhHant, Industry.MortgageRealEstateInvestmentTrusts, "按揭房地產投資信託基金"),
            FromEnum(zhHant, Industry.Insurance, "保險"),
            FromEnum(zhHant, Industry.ITServices, "資訊科技服務"),
            FromEnum(zhHant, Industry.Software, "軟體"),
            FromEnum(zhHant, Industry.CommunicationsEquipment, "通訊設備"),
            FromEnum(zhHant, Industry.TechnologyHardwareStorageAndPeripherals, "電腦硬體、儲存及週邊設備"),
            FromEnum(zhHant, Industry.ElectronicEquipmentInstrumentsAndComponents, "電子設備、儀器與零件"),
            FromEnum(zhHant, Industry.SemiconductorsAndSemiconductorEquipment, "半導體產品與設備"),
            FromEnum(zhHant, Industry.DiversifiedTelecommunicationServices, "綜合電訊服務"),
            FromEnum(zhHant, Industry.WirelessTelecommunicationServices, "無線電訊服務"),
            FromEnum(zhHant, Industry.Media, "媒體"),
            FromEnum(zhHant, Industry.Entertainment, "娛樂"),
            FromEnum(zhHant, Industry.InteractiveMediaAndServices, "互動媒體與服務"),
            FromEnum(zhHant, Industry.ElectricUtilities, "電力公用事業"),
            FromEnum(zhHant, Industry.GasUtilities, "燃氣公用事業"),
            FromEnum(zhHant, Industry.MultiUtilities, "複合型公用事業"),
            FromEnum(zhHant, Industry.WaterUtilities, "水務公用事業"),
            FromEnum(
                zhHant,
                Industry.IndependentPowerAndRenewableElectricityProducers,
                "獨立電力及可再生電力生產商"
            ),
            FromEnum(zhHant, Industry.DiversifiedReits, "多元化房地產投資信託基金"),
            FromEnum(zhHant, Industry.IndustrialReits, "工業房地產投資信託基金"),
            FromEnum(zhHant, Industry.HotelAndResortReits, "酒店及度假村房地產投資信託基金"),
            FromEnum(zhHant, Industry.OfficeReits, "辦公室房地產投資信託基金"),
            FromEnum(zhHant, Industry.HealthCareReits, "醫療保健房地產投資信託基金"),
            FromEnum(zhHant, Industry.ResidentialReits, "住宅房地產投資信託基金"),
            FromEnum(zhHant, Industry.RetailReits, "零售業房地產投資信託基金"),
            FromEnum(zhHant, Industry.SpecializedReits, "特種房地產投資信託基金"),
            FromEnum(zhHant, Industry.RealEstateManagementAndDevelopment, "房地產管理與開發"),
            FromModel(zhHant, "Exchange.OpeningTime", "開盤時間"),
            FromModel(zhHant, "Exchange.ClosingTime", "收盤時間"),
        };

        var results = GetCommonLocaleResources().Concat(resourcesEN).Concat(resourcesZHHant);

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
        exchangeXtai.UpdateName("臺灣證券交易所", CultureInfo.GetCultureInfo("zh-Hant"));

        var results = new[] { exchangeXtai, };

        return results;
    }
}
