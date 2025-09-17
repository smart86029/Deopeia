using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.GetPermission;

internal sealed class GetPermissionQueryHandler(IPermissionRepository permissionRepository)
    : IQueryHandler<GetPermissionQuery, GetPermissionResult>
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async ValueTask<GetPermissionResult> Handle(
        GetPermissionQuery query,
        CancellationToken cancellationToken
    )
    {
        var permissionCode = new PermissionCode(query.Code);
        var permission = await _permissionRepository.GetPermissionAsync(permissionCode);

        return new GetPermissionResult
        {
            Code = permission.Id.Value,
            IsEnabled = permission.IsEnabled,
            Localizations = permission
                .Localizations.Select(l => new PermissionLocalizationDto
                {
                    Culture = l.Culture.Name,
                    Name = l.Name,
                    Description = l.Description,
                })
                .ToList(),
        };
    }
}
