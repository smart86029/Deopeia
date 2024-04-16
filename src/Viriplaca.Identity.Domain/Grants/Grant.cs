using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.Domain.Grants;

public abstract class Grant : AggregateRoot
{
    protected Grant()
    {
    }

    protected Grant(GrantTypes type, Client client, TimeSpan expiresIn)
    {
        type.MustBeDefined();

        Type = type;
        ClientId = client.Id;
        ExpiresAt = CreatedAt.Add(expiresIn);
    }

    public string Key { get; private init; } = Guid.NewGuid().ToString("N");

    public GrantTypes Type { get; private init; }

    public Guid? SubjectId { get; private set; }

    public Guid ClientId { get; private init; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiresAt { get; private init; }

    public DateTimeOffset? ConsumedAt { get; private set; }

    public void UpdateSubjectId(Guid subjectId)
    {
        subjectId.MustNotBeEmpty();

        SubjectId = subjectId;
    }
}
