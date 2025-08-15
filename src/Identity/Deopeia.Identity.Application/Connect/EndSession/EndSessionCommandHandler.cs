using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Connect.EndSession;

internal class EndSessionCommandHandler(
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository
) : ICommandHandler<EndSessionCommand, EndSessionResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public async ValueTask<EndSessionResult> Handle(
        EndSessionCommand request,
        CancellationToken cancellationToken
    )
    {
        await _unitOfWork.CommitAsync();

        return new EndSessionResult(request.PostLogoutRedirectUri, request.State);
    }
}
