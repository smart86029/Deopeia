using Deopeia.Trading.Application.Contracts.GetContractOptions;

namespace Deopeia.Trading.Infrastructure.Contracts.GetContractOptions;

internal class GetContractOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetContractOptionsQuery, ICollection<OptionResult<string>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetContractOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    CONCAT(COALESCE(b.name, c.name), ' (', a.symbol, ')') AS name
FROM contract AS a
LEFT JOIN contract_locale AS b ON a.id = b.contract_id AND b.culture = @CurrentCulture
INNER JOIN contract_locale AS c ON a.id = c.contract_id AND c.culture = @DefaultThreadCurrentCulture
WHERE a.underlying_type = @UnderlyingType
ORDER BY a.symbol
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                request.UnderlyingType,
            }
        );

        return optoins.ToList();
    }
}
