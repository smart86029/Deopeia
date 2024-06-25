namespace Deopeia.Identity.Application.WellKnown.GetConfiguration;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
public class ConfigurationDto
{
    public string Issuer { get; set; } = string.Empty;

    public string AuthorizationEndpoint { get; set; } = string.Empty;

    public string TokenEndpoint { get; set; } = string.Empty;

    public IList<string> TokenEndpointAuthMethodsSupported { get; set; } = [];

    public IList<string> TokenEndpointAuthSigningAlgValuesSupported { get; set; } = [];

    public string UserinfoEndpoint { get; set; } = string.Empty;

    public string CheckSessionIframe { get; set; } = string.Empty;

    public string EndSessionEndpoint { get; set; } = string.Empty;

    public string JwksUri { get; set; } = string.Empty;

    public string RegistrationEndpoint { get; set; } = string.Empty;

    public IList<string> ScopesSupported { get; set; } = [];

    public IList<string> ResponseTypesSupported { get; set; } = [];

    public IList<string> AcrValuesSupported { get; set; } = [];

    public IList<string> SubjectTypesSupported { get; set; } = [];

    public IList<string> UserinfoSigningAlgValuesSupported { get; set; } = [];

    public IList<string> UserinfoEncryptionAlgValuesSupported { get; set; } = [];

    public IList<string> UserinfoEncryptionEncValuesSupported { get; set; } = [];

    public IList<string> IdTokenSigningAlgValuesSupported { get; set; } = [];

    public IList<string> IdTokenEncryptionAlgValuesSupported { get; set; } = [];

    public IList<string> IdTokenEncryptionEncValuesSupported { get; set; } = [];

    public IList<string> RequestObjectSigningAlgValuesSupported { get; set; } = [];

    public IList<string> DisplayValuesSupported { get; set; } = [];

    public IList<string> ClaimTypesSupported { get; set; } = [];

    public IList<string> ClaimsSupported { get; set; } = [];

    public bool ClaimsParameterSupported { get; set; }

    public string ServiceDocumentation { get; set; } = string.Empty;

    public IList<string> UiLocalesSupported { get; set; } = [];

    public string IntrospectionEndpoint { get; set; } = string.Empty;
}
