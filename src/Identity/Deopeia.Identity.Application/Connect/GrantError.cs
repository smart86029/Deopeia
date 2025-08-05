namespace Deopeia.Identity.Application.Connect;

internal enum GrantError
{
    InvalidRequest,

    InvalidClient,

    InvalidGrant,

    UnauthorizedClient,

    UnsupportedGrantType,

    InvalidScope,
}
