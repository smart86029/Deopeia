namespace Deopeia.Identity.Domain.Users;

public record TraderCreated(Guid UserId, string UserName) : DomainEvent { }
