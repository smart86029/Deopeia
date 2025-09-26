using Deopeia.Product.Application.Instruments.CreateInstrument;
using Deopeia.Product.Application.Instruments.DeleteInstrument;
using Deopeia.Product.Application.Instruments.GetInstrument;
using Deopeia.Product.Application.Instruments.GetInstruments;
using Deopeia.Product.Application.Instruments.UpdateInstrument;
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

    public override async Task<GetInstrumentResponse> GetInstrument(
        GetInstrumentRequest request,
        ServerCallContext context
    )
    {
        var query = request.Adapt<GetInstrumentQuery>();
        var instrument = await _mediator.Send(query);
        return instrument.Adapt<GetInstrumentResponse>();
    }

    public override async Task<Empty> CreateInstrument(
        CreateInstrumentRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<CreateInstrumentCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> UpdateInstrument(
        UpdateInstrumentRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<UpdateInstrumentCommand>();
        await _mediator.Send(command);
        return new Empty();
    }

    public override async Task<Empty> DeleteInstrument(
        DeleteInstrumentRequest request,
        ServerCallContext context
    )
    {
        var command = request.Adapt<DeleteInstrumentCommand>();
        await _mediator.Send(command);
        return new Empty();
    }
}
