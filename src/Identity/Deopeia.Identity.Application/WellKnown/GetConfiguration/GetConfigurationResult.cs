namespace Deopeia.Identity.Application.WellKnown.GetConfiguration;

public sealed record GetConfigurationResult
{
    public string Issuer { get; init; } = string.Empty;

    public string AuthorizationEndpoint { get; init; } = string.Empty;

    public string TokenEndpoint { get; init; } = string.Empty;

    public string? UserinfoEndpoint { get; init; }

    public string JwksUri { get; init; } = string.Empty;

    public string? RegistrationEndpoint { get; init; } = string.Empty;

    public IReadOnlyList<string>? ScopesSupported { get; init; }

    public IReadOnlyList<string> ResponseTypesSupported { get; init; } = [];

    public IReadOnlyList<string>? ResponseModesSupported { get; init; }

    public IReadOnlyList<string>? GrantTypesSupported { get; init; }

    public IReadOnlyList<string>? AcrValuesSupported { get; init; }

    public IReadOnlyList<string> SubjectTypesSupported { get; init; } = [];

    public IReadOnlyList<string> IdTokenSigningAlgValuesSupported { get; init; } = [];

    public IReadOnlyList<string>? IdTokenEncryptionAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? IdTokenEncryptionEncValuesSupported { get; init; }

    public IReadOnlyList<string>? UserinfoSigningAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? UserinfoEncryptionAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? UserinfoEncryptionEncValuesSupported { get; init; }

    public IReadOnlyList<string>? RequestObjectSigningAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? RequestObjectEncryptionAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? TokenEndpointAuthMethodsSupported { get; init; }

    public IReadOnlyList<string>? TokenEndpointAuthSigningAlgValuesSupported { get; init; }

    public IReadOnlyList<string>? DisplayValuesSupported { get; init; }

    public IReadOnlyList<string>? ClaimTypesSupported { get; init; }

    public IReadOnlyList<string>? ClaimsSupported { get; init; }

    public string? ServiceDocumentation { get; init; }

    public IReadOnlyList<string>? ClaimsLocalesSupported { get; init; }

    public IReadOnlyList<string>? UiLocalesSupported { get; init; }

    public bool ClaimsParameterSupported { get; init; }

    public bool RequestParameterSupported { get; init; }

    public bool RequestUriParameterSupported { get; init; } = true;

    public bool RequireRequestUriRegistration { get; init; }

    public string? OpPolicyUri { get; init; }

    public string? OpTosUri { get; init; }

    public string IntrospectionEndpoint { get; init; } = string.Empty;

    public string RevocationEndpoint { get; init; } = string.Empty;

    public string CheckSessionIframe { get; init; } = string.Empty;

    public string EndSessionEndpoint { get; set; } = string.Empty;
}
