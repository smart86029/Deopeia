namespace Deopeia.Quote.Domain.ContractSpecifications;

public class ContractSpecification
    : AggregateRoot<ContractSpecificationId>,
        ILocalizable<ContractSpecificationLocale, ContractSpecificationId>
{
    private readonly EntityLocaleCollection<
        ContractSpecificationLocale,
        ContractSpecificationId
    > _locales = [];

    private ContractSpecification() { }

    public ContractSpecification(
        InstrumentType type,
        ExchangeId exchangeId,
        string symbol,
        string symbolTemplate,
        string name,
        string nameTemplate,
        CurrencyCode currencyCode,
        AssetId underlyingAssetId,
        ContractSize contractSize,
        decimal tickSize
    )
    {
        type.MustBeDefined();
        symbol.MustNotBeNullOrWhiteSpace();
        symbolTemplate.MustNotBeNullOrWhiteSpace();

        Type = type;
        ExchangeId = exchangeId;
        Symbol = symbol;
        SymbolTemplate = symbolTemplate;
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateNameTemplate(nameTemplate);
        CurrencyCode = currencyCode;
        UnderlyingAssetId = underlyingAssetId;
        ContractSize = contractSize;
        TickSize = tickSize;
    }

    public InstrumentType Type { get; private init; }

    public ExchangeId ExchangeId { get; private init; }

    public string Symbol { get; private init; } = string.Empty;

    public string SymbolTemplate { get; private init; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string NameTemplate =>
        _locales[CultureInfo.CurrentCulture]?.NameTemplate ?? string.Empty;

    public CurrencyCode CurrencyCode { get; private init; }

    public AssetId UnderlyingAssetId { get; private init; }

    public ContractSize ContractSize { get; private init; }

    public decimal TickSize { get; private init; }

    public IReadOnlyCollection<ContractSpecificationLocale> Locales => _locales;

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateNameTemplate(string nameTemplate, CultureInfo culture)
    {
        _locales[culture].UpdateNameTemplate(nameTemplate);
    }
}
