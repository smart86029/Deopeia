using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Connect.Authorize;

internal class AuthorizeCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository
) : IRequestHandler<AuthorizeCommand, AuthorizeResult>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public async Task<AuthorizeResult> Handle(
        AuthorizeCommand request,
        CancellationToken cancellationToken
    )
    {
        if (request.ResponseType != "code")
        {
            throw new Exception("response type is required or is not valid");
        }

        var client = await _clientRepository.GetClientAsync(request.ClientId);
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
            string.Empty,
            request.CodeChallenge,
            request.CodeChallengeMethod
        );
        _authorizationCodeRepository.Add(authorizationCode);
        await _unitOfWork.CommitAsync();

        var result = new AuthorizeResult(authorizationCode, request.State);

        return result;
    }
}
