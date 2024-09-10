using Deopeia.Identity.Application.Permissions;
using Deopeia.Identity.Application.Permissions.GetPermission;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermission;

public class GetPermissionQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetPermissionQuery, GetPermissionViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetPermissionViewModel> Handle(
        GetPermissionQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    code,
    is_enabled
FROM permission
WHERE id = @Id;

SELECT
    culture,
    name,
    description
FROM permission_locale
WHERE permission_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetPermissionViewModel>();
        result.Locales = multiple.Read<PermissionLocaleDto>().ToList();

        return result;
    }
}
