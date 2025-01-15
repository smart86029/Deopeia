namespace Deopeia.Trading.Application.Traders.Dislike;

public record DislikeCommand(Guid Id, string Symbol) : IRequest { }
