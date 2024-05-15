namespace Viriplaca.Identity.App.Connect.RefreshTokenGrant;

public class RefreshTokenGrantResult : GrantResult
{
    internal RefreshTokenGrantResult() { }

    internal RefreshTokenGrantResult(GrantError error)
        : base(error) { }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; private set; } = "bearer";

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonIgnore]
    public TimeSpan Lifetime { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn => Lifetime.TotalSeconds.ToInt();
}
