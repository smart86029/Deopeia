using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.App.Connect.Authorize;

internal class AuthorizeCommandHandler(
    IClientRepository clientRepository)
    : IRequestHandler<AuthorizeCommand, AuthorizeResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<AuthorizeResult> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
    {
        if (request.ResponseType != "code")
        {
            throw new Exception("response type is required or is not valid");
        }

        var client = await _clientRepository.GetClientAsync(request.ClientId);
        if (!client.IsEnabled)
        {
            throw new Exception("UnAuthoriazedClient");
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

        var result = new AuthorizeResult
        {
            Code = Guid.NewGuid().ToString("N"),
            State = request.State,
        };

        return result;
    }
}
