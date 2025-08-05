namespace Deopeia.Identity.Application.WellKnown.GetConfiguration;

internal class GetConfigurationQueryHandler(IOptions<JwtOptions> jwtOptions)
    : IQueryHandler<GetConfigurationQuery, ConfigurationDto>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly Uri _issuerUri = new(jwtOptions.Value.Issuer);

    public ValueTask<ConfigurationDto> Handle(
        GetConfigurationQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = new ConfigurationDto
        {
            Issuer = _jwtOptions.Issuer,
            AuthorizationEndpoint = Relative("/Connect/Authorize"),
            TokenEndpoint = Relative("/Connect/Token"),
            TokenEndpointAuthMethodsSupported = new string[]
            {
                "client_secret_basic",
                "private_key_jwt",
            },
            TokenEndpointAuthSigningAlgValuesSupported = new string[] { "RS256", "ES256" },
            AcrValuesSupported = new string[]
            {
                "urn:mace:incommon:iap:silver",
                "urn:mace:incommon:iap:bronze",
            },
            ResponseTypesSupported = new string[]
            {
                "code",
                "code id_token",
                "id_token",
                "token id_token",
            },
            SubjectTypesSupported = new string[] { "public", "pairwise" },
            UserinfoEndpoint = Relative("/api/UserInfo/GetUserInfo"),
            UserinfoEncryptionEncValuesSupported = new string[] { "A128CBC-HS256", "A128GCM" },
            IdTokenSigningAlgValuesSupported = new string[] { "RS256", "ES256", "HS256", "SHA256" },
            IdTokenEncryptionAlgValuesSupported = new string[] { "RSA1_5", "A128KW" },
            IdTokenEncryptionEncValuesSupported = new string[] { "A128CBC-HS256", "A128GCM" },
            RequestObjectSigningAlgValuesSupported = new string[] { "none", "RS256", "ES256" },
            DisplayValuesSupported = new string[] { "page", "popup" },
            ClaimTypesSupported = new string[] { "normal", "distributed" },
            JwksUri = Relative("/jwks.json"),
            ScopesSupported = new[]
            {
                "openid",
                "profile",
                "email",
                "address",
                "phone",
                "offline_access",
            },
            ClaimsSupported = new[]
            {
                "sub",
                "iss",
                "auth_time",
                "acr",
                "name",
                "given_name",
                "family_name",
                "nickname",
                "profile",
                "picture",
                "website",
                "email",
                "email_verified",
                "locale",
                "zoneinfo",
            },
            ClaimsParameterSupported = true,
            ServiceDocumentation = Relative("/connect/service_documentation.html"),
            UiLocalesSupported = new string[] { "en", "zh-Hant" },
            IntrospectionEndpoint = Relative("/Introspections/TokenIntrospect"),
            RevocationEndpoint = Relative("/Revoke"),
            EndSessionEndpoint = Relative("/Connect/EndSession"),
        };

        return ValueTask.FromResult(result);
    }

    private string Relative(string relativePath)
    {
        return new Uri(_issuerUri, relativePath).ToString();
    }
}
