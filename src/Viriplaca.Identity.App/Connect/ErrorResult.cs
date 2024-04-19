namespace Viriplaca.Identity.App.Connect;

public class ErrorResult
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = GrantError.InvalidGrant.ToString().ToSnakeCaseLower();

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; } = string.Empty;

    [JsonPropertyName("error_uri")]
    public string ErrorUri { get; set; } = string.Empty;
}
