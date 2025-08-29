using Deopeia.Identity.Application.Roles.CreateRole;
using Deopeia.Identity.Application.Roles.DeleteRole;
using Deopeia.Identity.Application.Roles.GetRole;
using Deopeia.Identity.Application.Roles.GetRoleOptions;
using Deopeia.Identity.Application.Roles.GetRoles;
using Deopeia.Identity.Application.Roles.UpdateRole;
using Deopeia.Identity.Contracts;

namespace Deopeia.Identity.Api.Services;

public class RoleService(IMediator mediator) : Contracts.RoleService.RoleServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<ListRoleResponse> ListRole(
        ListRoleRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetRolesQuery>();
        var roles = await _mediator.Send(query);
        return roles.Adapt<ListRoleResponse>();
    }

    public override async Task<ListOptionResponse> ListRoleOption(
        Empty request,
        ServerCallContext context
    )
    {
        var query = new GetRoleOptionsQuery();
        var options = await _mediator.Send(query);
        return new ListOptionResponse
        {
            Items = { options.Select(o => o.Adapt<ListOptionResponse.Types.Option>()) },
        };
    }

    public override async Task<GetRoleResponse> GetRole(
        GetRoleRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetRoleQuery>();
        var role = await _mediator.Send(query);
        return role.Adapt<GetRoleResponse>();
    }

    public override async Task<Empty> CreateRole(
        CreateRoleRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<CreateRoleCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> UpdateRole(
        UpdateRoleRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<UpdateRoleCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> DeleteRole(
        DeleteRoleRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<DeleteRoleCommand>();
        await _mediator.Send(command);
        return new Empty();
    }
}
