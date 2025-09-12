namespace Deopeia.Identity.Application.WellKnown.GetConfiguration;

internal sealed class GetConfigurationQueryHandler(IOptions<JwtOptions> jwtOptions)
    : IQueryHandler<GetConfigurationQuery, GetConfigurationResult>
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly Uri _issuerUri = new(jwtOptions.Value.Issuer);

    public ValueTask<GetConfigurationResult> Handle(
        GetConfigurationQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = new GetConfigurationResult
        {
            Issuer = _jwtOptions.Issuer,
            AuthorizationEndpoint = Relative("/Connect/Authorize"),
            TokenEndpoint = Relative("/Connect/Token"),
            JwksUri = Relative("/.well-known/jwks.json"),
            ScopesSupported = ["openid", "profile", "email", "address", "phone", "offline_access"],
            ResponseTypesSupported = ["code", "id_token", "token id_token"],
            SubjectTypesSupported = ["pairwise", "public"],
            IdTokenSigningAlgValuesSupported = ["none", "RS256"],
            RequestObjectSigningAlgValuesSupported = ["none", "RS256"],
            TokenEndpointAuthMethodsSupported = ["client_secret_basic", "private_key_jwt"], // TODO: implement private_key_jwt
            TokenEndpointAuthSigningAlgValuesSupported = ["RS256"],
            DisplayValuesSupported = ["page", "popup"],
            ClaimTypesSupported = ["normal"],
            ClaimsSupported =
            [
                "sub",
                "name",
                "given_name",
                "family_name",
                "middle_name",
                "nickname",
                "preferred_username",
                "profile",
                "picture",
                "website",
                "email",
                "email_verified",
                "gender",
                "birthdate",
                "zoneinfo",
                "locale",
                "phone_number",
                "phone_number_verified",
                "address",
                "updated_at",
            ],
            UiLocalesSupported = ["en", "zh-Hant"],
            ClaimsParameterSupported = true,
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
