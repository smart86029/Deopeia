namespace Viriplaca.Identity.App.Connect.GenerateToken;

public class ErrorDto
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = TokenError.InvalidGrant.ToString().ToSnakeCaseLower();

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; } = string.Empty;

    [JsonPropertyName("error_uri")]
    public string ErrorUri { get; set; } = string.Empty;
}
