namespace Deopeia.Common.Domain.Finance;

public readonly record struct Money(CurrencyCode CurrencyCode, decimal Amount) { }
