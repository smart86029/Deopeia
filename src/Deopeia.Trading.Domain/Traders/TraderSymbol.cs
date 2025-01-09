namespace Deopeia.Trading.Domain.Traders;

public record TraderSymbol(TraderId TraderId, Symbol Symbol) : ValueObject { }
