namespace Deopeia.Identity.Domain.Users;

public record UserCreated(UserId UserId, string UserName) : DomainEvent { }
