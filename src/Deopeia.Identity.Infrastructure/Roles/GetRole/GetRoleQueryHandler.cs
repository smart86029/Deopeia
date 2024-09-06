using Deopeia.Identity.Application.Roles;
using Deopeia.Identity.Application.Roles.GetRole;

namespace Deopeia.Identity.Infrastructure.Roles.GetRole;

public class GetRoleQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetRoleQuery, GetRoleViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetRoleViewModel> Handle(
        GetRoleQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    is_enabled
FROM role
WHERE id = @Id;

SELECT
    culture,
    name,
    description
FROM role_locale
WHERE role_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetRoleViewModel>();
        result.Locales = multiple.Read<RoleLocaleDto>().ToList();

        return result;
    }
}
