namespace Deopeia.Identity.Domain.Users;

public record UserDisabled(Guid UserId) : DomainEvent { }
