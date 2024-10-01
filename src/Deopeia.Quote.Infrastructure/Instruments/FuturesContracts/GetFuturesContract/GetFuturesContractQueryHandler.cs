using Deopeia.Quote.Application.FuturesContracts;
using Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts.GetFuturesContract;

internal class GetFuturesContractQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetFuturesContractQuery, GetFuturesContractViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetFuturesContractViewModel> Handle(
        GetFuturesContractQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id,
    a.exchange_id,
    a.symbol,
    a.currency_code,
    b.underlying_asset_id,
    b.tick_size,
    b.contract_size_quantity,
    b.contract_size_unit_code
FROM instrument AS a
INNER JOIN contract_specification AS b ON a.contract_specification_id = b.id
WHERE a.id = @Id AND a.type = @Futures;

SELECT
    culture,
    name
FROM instrument_locale
WHERE instrument_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(
            sql,
            new { request.Id, InstrumentType.Futures }
        );
        var result = multiple.ReadFirst<GetFuturesContractViewModel>();
        result.Locales = multiple.Read<FuturesContractLocaleDto>().ToList();

        return result;
    }
}
