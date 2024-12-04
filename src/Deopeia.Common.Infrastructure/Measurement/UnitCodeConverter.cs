using Deopeia.Common.Domain.Measurement;

namespace Deopeia.Common.Infrastructure.Measurement;

internal class UnitCodeConverter()
    : ValueConverter<UnitCode, string>(unitCode => unitCode.Value, value => new UnitCode(value)) { }
