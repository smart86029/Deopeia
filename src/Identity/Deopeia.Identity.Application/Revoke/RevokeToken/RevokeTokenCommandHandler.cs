using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Application.Revoke.RevokeToken;

internal class RevokeTokenCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRefreshTokenRepository refreshTokenRepository
) : ICommandHandler<RevokeTokenCommand, RevokeTokenResult>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async ValueTask<RevokeTokenResult> Handle(
        RevokeTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        if (command.TokenTypeHint == "access_token") { }
        else if (command.TokenTypeHint == "refresh_token")
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(command.Token);
            if (refreshToken is not null)
            {
                refreshToken.Consume();
                await _unitOfWork.CommitAsync();
            }
        }

        return new RevokeTokenResult();
    }
}
