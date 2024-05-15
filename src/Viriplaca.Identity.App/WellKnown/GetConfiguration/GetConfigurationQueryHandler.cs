namespace Viriplaca.Identity.App.WellKnown.GetConfiguration;

internal class GetConfigurationQueryHandler
    : IRequestHandler<GetConfigurationQuery, ConfigurationDto>
{
    public Task<ConfigurationDto> Handle(
        GetConfigurationQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = new ConfigurationDto
        {
            Issuer = "https://localhost:7002",
            AuthorizationEndpoint = "https://localhost:7002/Connect/Authorize",
            TokenEndpoint = "https://localhost:7002/Connect/Token",
            TokenEndpointAuthMethodsSupported = new string[]
            {
                "client_secret_basic",
                "private_key_jwt"
            },
            TokenEndpointAuthSigningAlgValuesSupported = new string[] { "RS256", "ES256" },
            AcrValuesSupported = new string[]
            {
                "urn:mace:incommon:iap:silver",
                "urn:mace:incommon:iap:bronze"
            },
            ResponseTypesSupported = new string[]
            {
                "code",
                "code id_token",
                "id_token",
                "token id_token"
            },
            SubjectTypesSupported = new string[] { "public", "pairwise" },
            UserinfoEndpoint = "https://localhost:7002/api/UserInfo/GetUserInfo",
            UserinfoEncryptionEncValuesSupported = new string[] { "A128CBC-HS256", "A128GCM" },
            IdTokenSigningAlgValuesSupported = new string[] { "RS256", "ES256", "HS256", "SHA256" },
            IdTokenEncryptionAlgValuesSupported = new string[] { "RSA1_5", "A128KW" },
            IdTokenEncryptionEncValuesSupported = new string[] { "A128CBC-HS256", "A128GCM" },
            RequestObjectSigningAlgValuesSupported = new string[] { "none", "RS256", "ES256" },
            DisplayValuesSupported = new string[] { "page", "popup" },
            ClaimTypesSupported = new string[] { "normal", "distributed" },
            JwksUri = "https://localhost:7002/jwks.json",
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
            ServiceDocumentation = "https://localhost:7002/connect/service_documentation.html",
            UiLocalesSupported = new string[] { "en-US", "zh-TW" },
            IntrospectionEndpoint = "https://localhost:7002/Introspections/TokenIntrospect"
        };

        return Task.FromResult(result);
    }
}
