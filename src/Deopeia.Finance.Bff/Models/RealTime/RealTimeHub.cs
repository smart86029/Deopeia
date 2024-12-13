using System.Security.Claims;
using Deopeia.Finance.Bff.Models.Quotes;

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

        var claim = identity.Claims.FirstOrDefault(x => x.Type == ClaimType);
        if (claim is not null)
        {
            identity.RemoveClaim(claim);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, claim.Value);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, symbol);
        identity.AddClaim(new Claim(ClaimType, symbol));
    }
}
