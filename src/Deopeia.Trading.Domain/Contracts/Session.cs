namespace Deopeia.Trading.Domain.Contracts;

public record Session(
    Symbol Symbol,
    DayOfWeek OpenDay,
    TimeOnly OpenTime,
    DayOfWeek CloseDay,
    TimeOnly CloseTime
) : ValueObject { }
