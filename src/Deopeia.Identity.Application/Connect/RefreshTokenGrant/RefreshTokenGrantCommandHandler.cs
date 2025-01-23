using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.RefreshTokens;
using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Connect.RefreshTokenGrant;

internal class RefreshTokenGrantCommandHandler(
    IOptions<JwtOptions> jwtOptions,
    IIdentityUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IPermissionRepository permissionRepository,
    IRefreshTokenRepository refreshTokenRepository
)
    : GrantCommandHandler<RefreshTokenGrantCommand>(
        jwtOptions,
        unitOfWork,
        permissionRepository,
        refreshTokenRepository
    )
{
    private readonly TimeSpan _lifetime = TimeSpan.FromMinutes(5);
    private readonly IIdentityUnitOfWork _identityUnitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public override async Task<GrantResult> Handle(
        RefreshTokenGrantCommand request,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(request.ClientId);
        if (
            client is null
            || !client.IsEnabled
            || !client.GrantTypes.HasFlag(GrantTypes.RefreshToken)
        )
        {
            return new RefreshTokenGrantResult(GrantError.UnauthorizedClient);
        }

        if (request.RefreshToken.IsNullOrWhiteSpace())
        {
            return new RefreshTokenGrantResult(GrantError.InvalidGrant);
        }

        var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken);
        if (refreshToken is null)
        {
            return new RefreshTokenGrantResult(GrantError.InvalidGrant);
        }

        if (refreshToken.ClientId != client.Id)
        {
            return new RefreshTokenGrantResult(GrantError.InvalidGrant);
        }

        if (refreshToken.IsExpired)
        {
            return new RefreshTokenGrantResult(GrantError.InvalidGrant);
        }

        if (refreshToken.Scopes.Count == 0)
        {
            return new RefreshTokenGrantResult(GrantError.InvalidRequest);
        }

        await ConsumeAsync(refreshToken);

        var accessToken = await GenerateAccessTokenAsync(refreshToken);
        var newRefreshToken = await GenerateRefreshTokenAsync(client, refreshToken);
        var result = new RefreshTokenGrantResult
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            Lifetime = _lifetime,
        };

        return result;
    }

    private async Task ConsumeAsync(RefreshToken refreshToken)
    {
        refreshToken.Consume();
        _refreshTokenRepository.Update(refreshToken);
        await _identityUnitOfWork.CommitAsync();
    }
}
