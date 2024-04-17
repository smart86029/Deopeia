namespace Viriplaca.Identity.App.Connect.GenerateToken;

internal enum TokenError
{
    InvalidRequest,

    InvalidClient,

    InvalidGrant,

    UnauthorizedClient,

    UnsupportedGrantType,

    InvalidScope,
}
