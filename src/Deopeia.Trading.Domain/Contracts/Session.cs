namespace Deopeia.Trading.Domain.Contracts;

public record Session(Symbol Symbol, DayOfWeek DayOfWeek, TimeOnly OpenTime, TimeOnly CloseTime)
    : ValueObject { }
