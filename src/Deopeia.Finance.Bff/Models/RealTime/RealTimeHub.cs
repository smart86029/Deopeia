using System.Security.Claims;

namespace Deopeia.Finance.Bff.Models.RealTime;

public class RealTimeHub : Hub<IRealTime>
{
    private const string ClaimType = "Symbol";

    public async Task ChangeSymbol(string symbol)
    {
        var identity = Context.User!.Identities.FirstOrDefault(x => x.Name == "SignalR");
        if (identity is null)
        {
            identity = new();
            Context.User.AddIdentity(identity);
        }

        var claims = identity.Claims.Where(x => x.Type == ClaimType);
        foreach (var claim in claims)
        {
            identity.RemoveClaim(claim);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, claim.Value);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
        identity.AddClaim(new Claim(ClaimType, symbol));
    }
}
