using Deopeia.Common.Domain.Measurement;

namespace Deopeia.Common.Infrastructure.Measurement;

internal class UnitCodeConverter()
    : ValueConverter<UnitCode, string>(
        currencyCode => currencyCode.Value,
        value => new UnitCode(value)
    ) { }
