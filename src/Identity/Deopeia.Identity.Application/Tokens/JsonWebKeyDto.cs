namespace Deopeia.Identity.Application.Tokens;

public sealed record JsonWebKeyDto(
    string Kty,
    string Use,
    string Alg,
    string Kid,
    string N,
    string E
);
