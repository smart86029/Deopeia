using Deopeia.Identity.Application.Tokens;
using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Application.Connect.RefreshTokenGrant;

internal class RefreshTokenGrantCommandHandler(
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IRefreshTokenRepository refreshTokenRepository
)
{
    private readonly TimeSpan _lifetime = TimeSpan.FromMinutes(5);
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async ValueTask<GrantResult> Handle(
        RefreshTokenGrantCommand command,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(command.ClientId);
        if (
            client is null
            || !client.IsEnabled
            || !client.GrantTypes.HasFlag(GrantTypes.RefreshToken)
        )
        {
            return new RefreshTokenGrantResult(GrantError.UnauthorizedClient);
        }

        if (command.RefreshToken.IsNullOrWhiteSpace())
        {
            return new RefreshTokenGrantResult(GrantError.InvalidGrant);
        }

        var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(command.RefreshToken);
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

        var accessToken = await _tokenService.GenerateAccessTokenAsync(refreshToken);
        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(refreshToken, client);
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
        await _unitOfWork.CommitAsync();
    }
}
