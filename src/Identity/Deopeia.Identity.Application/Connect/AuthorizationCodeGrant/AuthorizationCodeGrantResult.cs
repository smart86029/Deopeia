namespace Deopeia.Identity.Application.Connect.AuthorizationCodeGrant;

public class AuthorizationCodeGrantResult : GrantResult
{
    internal AuthorizationCodeGrantResult() { }

    internal AuthorizationCodeGrantResult(GrantError error)
        : base(error) { }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; private set; } = "bearer";

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("id_token")]
    public string IdToken { get; set; } = string.Empty;

    [JsonIgnore]
    public TimeSpan Lifetime { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn => Lifetime.TotalSeconds.ToInt();
}
