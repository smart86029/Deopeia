using Deopeia.Identity.Application.Permissions.CreatePermission;
using Deopeia.Identity.Application.Permissions.DeletePermission;
using Deopeia.Identity.Application.Permissions.GetPermission;
using Deopeia.Identity.Application.Permissions.GetPermissionOptions;
using Deopeia.Identity.Application.Permissions.GetPermissions;
using Deopeia.Identity.Application.Permissions.UpdatePermission;
using Deopeia.Identity.Contracts;

namespace Deopeia.Identity.Api.Services;

public class PermissionService(IMediator mediator)
    : Contracts.PermissionService.PermissionServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<ListPermissionResponse> ListPermission(
        ListPermissionRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetPermissionsQuery>();
        var permissions = await _mediator.Send(query);
        return permissions.Adapt<ListPermissionResponse>();
    }

    public override async Task<ListOptionResponse> ListPermissionOption(
        Empty request,
        ServerCallContext context
    )
    {
        var query = new GetPermissionOptionsQuery();
        var options = await _mediator.Send(query);
        return new ListOptionResponse
        {
            Items = { options.Select(o => o.Adapt<ListOptionResponse.Types.Option>()) },
        };
    }

    public override async Task<GetPermissionResponse> GetPermission(
        GetPermissionRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetPermissionQuery>();
        var permission = await _mediator.Send(query);
        return permission.Adapt<GetPermissionResponse>();
    }

    public override async Task<Empty> CreatePermission(
        CreatePermissionRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<CreatePermissionCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> UpdatePermission(
        UpdatePermissionRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<UpdatePermissionCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> DeletePermission(
        DeletePermissionRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<DeletePermissionCommand>();
        await _mediator.Send(command);
        return new Empty();
    }
}
