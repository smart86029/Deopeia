namespace Deopeia.Trading.Application.Traders.Like;

public record LikeCommand(Guid Id, string Symbol) : IRequest { }
