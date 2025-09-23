using Deopeia.Product.Application.Instruments.GetInstruments;
using Deopeia.Product.Contracts;

namespace Deopeia.Product.Api.Services;

public class InstrumentService(IMediator mediator)
    : Contracts.InstrumentService.InstrumentServiceBase
{
    private readonly IMediator _mediator = mediator;

    public override async Task<ListInstrumentResponse> ListInstrument(
        ListInstrumentRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetInstrumentsQuery>();
        var instruments = await _mediator.Send(query);
        return instruments.Adapt<ListInstrumentResponse>();
    }
}
