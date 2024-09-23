using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Finance;

internal class CurrencyCodeConverter()
    : ValueConverter<CurrencyCode, string>(
        currencyCode => currencyCode.Value,
        value => new CurrencyCode(value)
    ) { }
