namespace Viriplaca.Identity.App.Connect.GenerateToken;

public class TokenResult
{
    public string? AccessToken { get; set; }

    public string TokenType { get; private set; } = "bearer";

    public string IdToken { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    [JsonIgnore]
    public TimeSpan Expired { get; set; }

    public int ExpiresIn => Expired.TotalSeconds.ToInt();

    [JsonIgnore]
    public string QueryString => $"access_token={AccessToken}&token_type={TokenType}&id_token={IdToken}&state={State}&expires_in={ExpiresIn}";
}
