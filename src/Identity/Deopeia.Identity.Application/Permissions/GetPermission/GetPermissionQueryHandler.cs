using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.GetPermission;

internal sealed class GetPermissionQueryHandler(IPermissionRepository permissionRepository)
    : IQueryHandler<GetPermissionQuery, GetPermissionViewModel>
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async ValueTask<GetPermissionViewModel> Handle(
        GetPermissionQuery query,
        CancellationToken cancellationToken
    )
    {
        var permissionCode = new PermissionCode(query.Code);
        var permission = await _permissionRepository.GetPermissionAsync(permissionCode);

        return new GetPermissionViewModel
        {
            Code = permission.Id.Value,
            IsEnabled = permission.IsEnabled,
            Locales = permission
                .Locales.Select(l => new PermissionLocaleDto
                {
                    Culture = l.Culture.Name,
                    Name = l.Name,
                    Description = l.Description,
                })
                .ToList(),
        };
    }
}
