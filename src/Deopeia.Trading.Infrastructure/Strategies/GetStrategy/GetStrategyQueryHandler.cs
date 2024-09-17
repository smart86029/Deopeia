using Deopeia.Trading.Application.Strategies;
using Deopeia.Trading.Application.Strategies.GetStrategy;

namespace Deopeia.Trading.Infrastructure.Strategies.GetStrategy;

public class GetStrategyQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetStrategyQuery, GetStrategyViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetStrategyViewModel> Handle(
        GetStrategyQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    is_enabled,
    open_expression,
    close_expression
FROM strategy
WHERE id = @Id;

SELECT
    culture,
    name,
    description
FROM strategy_locale
WHERE strategy_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetStrategyViewModel>();
        result.Locales = multiple.Read<StrategyLocaleDto>().ToList();

        return result;
    }
}
