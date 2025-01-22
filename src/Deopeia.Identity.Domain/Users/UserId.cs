namespace Deopeia.Identity.Domain.Users;

public readonly record struct UserId(Guid Guid) : IEntityId
{
    public UserId()
        : this(Guid.CreateVersion7()) { }

    public override string ToString()
    {
        return Guid.ToString();
    }
}
