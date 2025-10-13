using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Finance;

internal sealed class AmountConverter()
    : ValueConverter<Amount, decimal>(amount => amount.Value, value => new Amount(value)) { }
