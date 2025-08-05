using Deopeia.Common.Domain.Measurement;

namespace Deopeia.Common.Domain.Finance;

public readonly record struct ContractSize(decimal Quantity, UnitCode UnitCode) { }
