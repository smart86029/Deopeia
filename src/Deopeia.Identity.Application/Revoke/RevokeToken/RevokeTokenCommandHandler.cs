using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Application.Revoke.RevokeToken;

internal class RevokeTokenCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRefreshTokenRepository refreshTokenRepository
) : IRequestHandler<RevokeTokenCommand, RevokeTokenResult>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<RevokeTokenResult> Handle(
        RevokeTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.TokenTypeHint == "access_token") { }
        else if (request.TokenTypeHint == "refresh_token")
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.Token);
            if (refreshToken is not null)
            {
                refreshToken.Consume();
                await _unitOfWork.CommitAsync();
            }
        }

        return new RevokeTokenResult();
    }
}
