using Deopeia.Identity.Application.Connect;

namespace Deopeia.Identity.Application.Revoke.RevokeToken;

public class RevokeTokenResult
{
    public RevokeTokenResult() { }

    internal RevokeTokenResult(GrantError error)
    {
        Error = new ErrorResult { Error = error.ToString().ToSnakeCaseLower() };
    }

    [JsonIgnore]
    public ErrorResult? Error { get; set; }
}
