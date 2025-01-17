namespace Deopeia.Trading.Application.Traders.TraderCreated;

public record TraderCreatedEvent(Guid UserId, string UserName) : Event { }
