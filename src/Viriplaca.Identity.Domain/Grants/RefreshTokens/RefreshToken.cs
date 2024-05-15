using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.Domain.Grants.RefreshTokens;

public class RefreshToken : Grant
{
    private RefreshToken() { }

    public RefreshToken(Guid subjectId, Client client, IEnumerable<string> scopes)
        : base(GrantTypes.RefreshToken, subjectId, client, scopes, TimeSpan.FromDays(1)) { }
}
