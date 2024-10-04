using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecificationOptions;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications.GetContractSpecificationOptions;

internal class GetContractSpecificationOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetContractSpecificationOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetContractSpecificationOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    COALESCE(b.name, c.name) AS name
FROM contract_specification AS a
LEFT JOIN contract_specification_locale AS b ON a.id = b.contract_specification_id AND b.culture = @CurrentCulture
INNER JOIN contract_specification_locale AS c ON a.id = c.contract_specification_id AND c.culture = @DefaultThreadCurrentCulture
WHERE a.type = @Futures AND a.underlying_asset_id = @AssetId
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
