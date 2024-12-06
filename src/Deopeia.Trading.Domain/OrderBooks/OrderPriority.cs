namespace Deopeia.Trading.Domain.OrderBooks;

public readonly record struct OrderPriority(decimal Price, DateTimeOffset CreatedAt) { }
