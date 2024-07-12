using System.Globalization;
using Deopeia.Quote.Application.Instruments.GetInstrument;

namespace Deopeia.Quote.Infrastructure.Instruments.GetInstrument;

internal class GetInstrumentQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetInstrumentQuery, GetInstrumentViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetInstrumentViewModel> Handle(
        GetInstrumentQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a."Symbol",
    b."Name"
FROM "Quote"."Instrument" AS a
INNER JOIN "Quote"."InstrumentLocale" AS b ON a."Id" = b."InstrumentId" AND b."Culture" = @Culture
WHERE a."Symbol" = @Symbol
""";
        var result = await _connection.QuerySingleAsync<GetInstrumentViewModel>(
            sql,
            new { request.Symbol, Culture = CultureInfo.CurrentCulture }
        );

        return result;
    }
}
