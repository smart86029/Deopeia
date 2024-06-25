namespace Deopeia.Identity.Domain.Grants;

[Flags]
public enum GrantTypes
{
    AuthorizationCode = 1 << 0,

    ClientCredentials = 1 << 1,

    RefreshToken = 1 << 2,

    Extension = 1 << 3,
}
