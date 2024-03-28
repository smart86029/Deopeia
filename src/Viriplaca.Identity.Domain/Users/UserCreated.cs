namespace Viriplaca.Identity.Domain.Users;

public record UserCreated(Guid UserId, string UserName)
    : DomainEvent
{
}
