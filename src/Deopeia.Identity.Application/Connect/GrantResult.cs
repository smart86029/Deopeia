using Deopeia.Identity.Application.Connect;

namespace Deopeia.Identity.Application.Connect;

public abstract class GrantResult
{
    public GrantResult() { }

    internal GrantResult(GrantError error)
    {
        Error = new ErrorResult { Error = error.ToString().ToSnakeCaseLower(), };
    }

    [JsonIgnore]
    public ErrorResult? Error { get; set; }
}
