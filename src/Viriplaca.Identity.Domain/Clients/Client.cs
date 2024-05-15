using Viriplaca.Identity.Domain.Grants;

namespace Viriplaca.Identity.Domain.Clients;

public class Client : AggregateRoot
{
    private readonly List<string> _scopes = [];
    private readonly List<Uri> _redirectUris = [];

    private Client() { }

    public Client(
        string name,
        string? secret,
        GrantTypes grantTypes,
        IEnumerable<string> scopes,
        IEnumerable<Uri> redirectUris
    )
    {
        name.MustNotBeNullOrWhiteSpace();
        grantTypes.MustBeDefined();

        Name = name;
        Secret = secret;
        GrantTypes = grantTypes;
        _scopes.AddRange(scopes.OrderBy(x => x));
        _redirectUris.AddRange(redirectUris.OrderBy(x => x.ToString()));
    }

    public string Name { get; private set; } = string.Empty;

    public string? Secret { get; private set; }

    public bool IsEnabled { get; private set; } = true;

    public GrantTypes GrantTypes { get; private set; }

    public IReadOnlyCollection<string> Scopes => _scopes.AsReadOnly();

    public IReadOnlyCollection<Uri> RedirectUris => _redirectUris.AsReadOnly();
}
