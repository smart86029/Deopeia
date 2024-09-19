using Deopeia.Quote.Application.FuturesContracts.GetFuturesContracts;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts.GetFuturesContracts;

internal class GetFuturesContractsQueryHandler(
    NpgsqlConnection connection,
    IStringLocalizer localizer
) : IRequestHandler<GetFuturesContractsQuery, PageResult<FuturesContractDto>>
{
    private readonly NpgsqlConnection _connection = connection;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<PageResult<FuturesContractDto>> Handle(
        GetFuturesContractsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("a.type = @Futures", new { InstrumentType.Futures });

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM instrument AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<FuturesContractDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.exchange_id,
    a.symbol,
    b.name,
    a.currency,
    a.underlying_asset_id
FROM instrument AS a
INNER JOIN instrument_locale AS b ON a.id = b.instrument_id AND b.culture = @CurrentCulture
/**where**/
ORDER BY a.exchange_id, a.symbol
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                CultureInfo.CurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var futuresContracts = await _connection.QueryAsync<FuturesContractDto>(
            sql.RawSql,
            sql.Parameters
        );
        result.Items = futuresContracts.ToList();

        return result;
    }
}
