using Deopeia.Identity.Application.Roles;
using Deopeia.Identity.Application.Roles.GetRole;

namespace Deopeia.Identity.Infrastructure.Roles.GetRole;

public class GetRoleQueryService(NpgsqlConnection connection) : IGetRoleQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetRoleResult> GetAsync(GetRoleQuery query)
    {
        var sql = """
SELECT
    code,
    is_enabled
FROM role
WHERE code = @Code;

SELECT
    culture,
    name,
    description
FROM role_localization
WHERE role_code = @Code;

SELECT permission_code
FROM role_permission
WHERE role_code = @Code;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, query);
        var result = multiple.ReadFirst<GetRoleResult>();
        result.Localizations = multiple.Read<RoleLocalizationDto>().ToList();
        result.PermissionCodes = multiple.Read<string>().ToList();
        return result;
    }
}
