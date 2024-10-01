using Deopeia.Quote.Application.FuturesContracts.GetFuturesContractOptions;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts.GetFuturesContractOptions;

internal class GetFuturesContractOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetFuturesContractOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetFuturesContractOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    COALESCE(b.name, c.name) AS name
FROM instrument AS a
LEFT JOIN instrument_locale AS b ON a.id = b.instrument_id AND b.culture = @CurrentCulture
INNER JOIN instrument_locale AS c ON a.id = c.instrument_id AND c.culture = @DefaultThreadCurrentCulture
INNER JOIN contract_specification AS d ON a.contract_specification_id = d.id
WHERE a.type = @Futures AND d.underlying_asset_id = @AssetId AND a.expiration_date > NOW()
ORDER BY a.symbol
""";
        var optoins = await _connection.QueryAsync<OptionResult<Guid>>(
            sql,
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                InstrumentType.Futures,
                request.AssetId,
            }
        );

        return optoins.ToList();
    }
}
