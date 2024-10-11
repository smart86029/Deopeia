using Deopeia.Quote.Domain.ContractSpecifications;

namespace Deopeia.Quote.Domain.Instruments.FuturesContracts;

public class FuturesContract : Instrument
{
    private static readonly Dictionary<int, char> MonthCodes =
        new()
        {
            [01] = 'F',
            [02] = 'G',
            [03] = 'H',
            [04] = 'J',
            [05] = 'K',
            [06] = 'M',
            [07] = 'N',
            [08] = 'Q',
            [09] = 'U',
            [10] = 'V',
            [11] = 'X',
            [12] = 'Z',
        };

    private FuturesContract() { }

    public FuturesContract(ContractSpecification contractSpecification, DateOnly expirationDate)
        : base(
            InstrumentType.Futures,
            contractSpecification.ExchangeId,
            FormatSymbol(
                contractSpecification.SymbolTemplate,
                contractSpecification.Symbol,
                expirationDate
            ),
            FormatName(
                contractSpecification.NameTemplate,
                contractSpecification.Name,
                expirationDate
            ),
            contractSpecification.CurrencyCode
        )
    {
        ContractSpecificationId = contractSpecification.Id;
        ExpirationDate = expirationDate;

        foreach (var locale in contractSpecification.Locales)
        {
            if (!locale.NameTemplate.IsNullOrWhiteSpace())
            {
                UpdateName(
                    FormatName(locale.NameTemplate, locale.Name, expirationDate),
                    locale.Culture
                );
            }
        }
    }

    public ContractSpecificationId ContractSpecificationId { get; private init; }

    public DateOnly ExpirationDate { get; private set; }

    public void UpdateExpirationDate(DateOnly expirationDate)
    {
        ExpirationDate = expirationDate;
    }

    private static string FormatSymbol(
        string symbolTemplate,
        string symbol,
        DateOnly expirationDate
    )
    {
        var values = new Dictionary<string, object>
        {
            ["Symbol"] = symbol,
            ["Date"] = expirationDate,
            ["MonthCode"] = MonthCodes[expirationDate.Month],
        };

        return symbolTemplate.Format(values);
    }

    private static string FormatName(string nameTemplate, string name, DateOnly expirationDate)
    {
        var values = new Dictionary<string, object> { ["Name"] = name, ["Date"] = expirationDate };

        return nameTemplate.Format(values);
    }
}
