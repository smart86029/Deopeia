using System.Text.Json.Serialization;

namespace Deopeia.Identity.Api.Controllers;

[Route("[controller]")]
[EnableCors(CorsPolicies.Oidc)]
public abstract class OidcController : ControllerBase
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    protected JsonResult Json(object? value)
    {
        return new JsonResult(value, JsonSerializerOptions);
    }
}
