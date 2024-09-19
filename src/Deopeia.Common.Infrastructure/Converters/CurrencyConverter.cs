using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Converters;

internal class CurrencyConverter()
    : ValueConverter<Currency, string>(id => id.Code, code => new Currency(code)) { }
