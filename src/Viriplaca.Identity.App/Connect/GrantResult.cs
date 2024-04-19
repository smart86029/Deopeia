namespace Viriplaca.Identity.App.Connect;

public abstract class GrantResult
{
    public GrantResult()
    {
    }

    internal GrantResult(GrantError error)
    {
        Error = new ErrorResult
        {
            Error = error.ToString().ToSnakeCaseLower(),
        };
    }

    [JsonIgnore]
    public ErrorResult? Error { get; set; }
}
