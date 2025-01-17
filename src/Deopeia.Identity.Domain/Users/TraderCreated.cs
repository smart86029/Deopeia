namespace Deopeia.Identity.Domain.Users;

public record TraderCreated(UserId UserId, string UserName) : DomainEvent { }
