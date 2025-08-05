namespace Deopeia.Identity.Domain.Users;

public record UserDisabled(UserId UserId) : DomainEvent { }
