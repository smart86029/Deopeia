namespace Deopeia.Product.Application.Instruments.GetInstruments;

internal sealed class GetInstrumentsQueryHandler(IGetInstrumentsQueryService queryService)
    : IQueryHandler<GetInstrumentsQuery, PagedResult<InstrumentDto>>
{
    private readonly IGetInstrumentsQueryService _queryService = queryService;

    public async ValueTask<PagedResult<InstrumentDto>> Handle(
        GetInstrumentsQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
