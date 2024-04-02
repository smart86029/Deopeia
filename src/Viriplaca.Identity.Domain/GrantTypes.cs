namespace Viriplaca.Identity.Domain;

[Flags]
public enum GrantTypes
{
    Code = 1 << 0,

    Implicit = 1 << 1,
}
