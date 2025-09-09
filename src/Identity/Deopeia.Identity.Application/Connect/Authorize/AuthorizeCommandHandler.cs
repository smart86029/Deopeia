using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Connect.Authorize;

internal class AuthorizeCommandHandler(
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository
) : ICommandHandler<AuthorizeCommand, AuthorizeResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public async ValueTask<AuthorizeResult> Handle(
        AuthorizeCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.ResponseType != "code")
        {
            throw new Exception("response type is required or is not valid");
        }

        var client = await _clientRepository.GetClientAsync(
            new ClientId(request.ClientId.ToGuid())
        );
        if (client is null || !client.IsEnabled)
        {
            throw new Exception("UnAuthoriazedClient");
        }

        if (request.CodeChallenge.IsNullOrWhiteSpace())
        {
            throw new Exception("code challenge required");
        }

        if (!client.RedirectUris.Contains(request.RedirectUri))
        {
            throw new Exception("redirect uri is not matched the one in the client store");
        }

        var clientScopes = client.Scopes.Where(request.Scopes.Contains);
        if (!clientScopes.Any())
        {
            throw new Exception("invalid scopes");
        }

        var authorizationCode = new AuthorizationCode(
            request.SubjectId,
            client,
            clientScopes,
            request.RedirectUri!,
            request.Nonce,
            request.CodeChallenge,
            request.CodeChallengeMethod
        );
        _authorizationCodeRepository.Add(authorizationCode);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new AuthorizeResult(authorizationCode, request.State);
    }
}
