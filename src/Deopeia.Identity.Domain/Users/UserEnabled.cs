namespace Deopeia.Identity.Domain.Users;

public record UserEnabled(Guid UserId) : DomainEvent { }
