using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Domain.Grants;

public abstract class Grant : AggregateRoot<GrantId>
{
    private readonly List<string> _scopes = [];

    protected Grant() { }

    protected Grant(
        GrantTypes type,
        Guid? subjectId,
        Client client,
        IEnumerable<string> scopes,
        TimeSpan expiresIn
    )
    {
        type.MustBeDefined();

        Type = type;
        SubjectId = subjectId;
        ClientId = client.Id;
        _scopes.AddRange(scopes);
        ExpiresAt = CreatedAt.Add(expiresIn);
    }

    public GrantTypes Type { get; private init; }

    public string Key { get; private init; } = Guid.NewGuid().ToString("N");

    public Guid? SubjectId { get; private set; }

    public ClientId ClientId { get; private init; }

    public IReadOnlyCollection<string> Scopes => _scopes.AsReadOnly();

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset ExpiresAt { get; private init; }

    public DateTimeOffset? ConsumedAt { get; private set; }

    public bool IsExpired => ExpiresAt.IsBeforeNow() || ConsumedAt.HasValue;

    public void UpdateSubjectId(Guid subjectId)
    {
        subjectId.MustNotBeEmpty();

        SubjectId = subjectId;
    }

    public void Consume()
    {
        ConsumedAt = DateTimeOffset.UtcNow;
    }
}
