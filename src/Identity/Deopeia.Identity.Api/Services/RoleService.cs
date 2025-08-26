using Deopeia.Identity.Application.Roles.GetRoleOptions;
using Deopeia.Identity.Contracts;

namespace Deopeia.Identity.Api.Services;

public class RoleService(IMediator mediator) : Contracts.RoleService.RoleServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override Task<ListRoleResponse> ListRole(
        ListRoleRequest request,
        ServerCallContext context
    )
    {
        return base.ListRole(request, context);
    }

    public override async Task<ListRoleOptionResponse> ListRoleOption(
        Empty request,
        ServerCallContext context
    )
    {
        var query = new GetRoleOptionsQuery();
        var options = await _mediator.Send(query);
        return new ListRoleOptionResponse
        {
            Items =
            {
                options.Select(o => new ListRoleOptionResponse.Types.RoleOption
                {
                    Name = o.Name,
                    Value = o.Value,
                    IsEnabled = o.IsEnabled,
                }),
            },
        };
    }

    public override Task<GetRoleResponse> GetRole(GetRoleRequest request, ServerCallContext context)
    {
        return base.GetRole(request, context);
    }
}
