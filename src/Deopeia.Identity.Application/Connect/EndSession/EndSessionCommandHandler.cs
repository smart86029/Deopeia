using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Connect.EndSession;

internal class EndSessionCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository
) : IRequestHandler<EndSessionCommand, EndSessionResult>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public async Task<EndSessionResult> Handle(
        EndSessionCommand request,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.CommitAsync();

        return new EndSessionResult(request.PostLogoutRedirectUri, request.State);
    }
}
