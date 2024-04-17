namespace Viriplaca.Identity.App.Connect.GenerateToken;

public class TokenResult
{
    public TokenResult()
    {
    }

    internal TokenResult(TokenError error)
    {
        Error = new ErrorDto
        {
            Error = error.ToString().ToSnakeCaseLower(),
        };
    }

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

    [JsonIgnore]
    public ErrorDto? Error { get; set; }
}
