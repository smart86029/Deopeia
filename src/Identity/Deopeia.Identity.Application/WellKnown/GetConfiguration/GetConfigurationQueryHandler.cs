namespace Deopeia.Identity.Application.WellKnown.GetConfiguration;

internal sealed class GetConfigurationQueryHandler(IOptions<JwtOptions> jwtOptions)
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
            TokenEndpointAuthMethodsSupported = ["client_secret_basic", "private_key_jwt"],
            TokenEndpointAuthSigningAlgValuesSupported = ["RS256", "ES256"],
            AcrValuesSupported = ["urn:mace:incommon:iap:silver", "urn:mace:incommon:iap:bronze"],
            ResponseTypesSupported = ["code", "code id_token", "id_token", "token id_token"],
            SubjectTypesSupported = ["public", "pairwise"],
            // UserinfoEndpoint = Relative("/UserInfo"),
            UserinfoEncryptionEncValuesSupported = ["A128CBC-HS256", "A128GCM"],
            IdTokenSigningAlgValuesSupported = ["RS256", "ES256", "HS256", "SHA256"],
            IdTokenEncryptionAlgValuesSupported = ["RSA1_5", "A128KW"],
            IdTokenEncryptionEncValuesSupported = ["A128CBC-HS256", "A128GCM"],
            RequestObjectSigningAlgValuesSupported = ["none", "RS256", "ES256"],
            DisplayValuesSupported = ["page", "popup"],
            ClaimTypesSupported = ["normal", "distributed"],
            JwksUri = Relative("/.well-known/jwks.json"),
            ScopesSupported = ["openid", "profile", "email", "address", "phone", "offline_access"],
            ClaimsSupported =
            [
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
            ],
            ClaimsParameterSupported = true,
            // ServiceDocumentation = Relative("/connect/service_documentation.html"),
            UiLocalesSupported = ["en", "zh-Hant"],
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
