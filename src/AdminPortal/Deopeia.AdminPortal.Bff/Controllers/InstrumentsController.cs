using Deopeia.AdminPortal.Bff.Models.Instruments;
using Deopeia.Product.Contracts;

namespace Deopeia.AdminPortal.Bff.Controllers;

public class InstrumentsController(InstrumentService.InstrumentServiceClient client) : ApiController
{
    private readonly InstrumentService.InstrumentServiceClient _client = client;

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Instrument>>> Get([FromQuery] GetRequest request)
    {
        var grpcRequest = request.Adapt<ListInstrumentRequest>();
        var grpcResponse = await _client.ListInstrumentAsync(grpcRequest);
        var response = grpcResponse.Adapt<PagedResponse<Instrument>>();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InstrumentResponse>> Get([FromRoute] Guid id)
    {
        var grpcRequest = new GetInstrumentRequest { Id = id };
        var grpcResponse = await _client.GetInstrumentAsync(grpcRequest);
        var response = grpcResponse.Adapt<InstrumentResponse>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRequest request)
    {
        var grpcRequest = request.Adapt<CreateInstrumentRequest>();
        await _client.CreateInstrumentAsync(grpcRequest);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var grpcRequest = request.Adapt<UpdateInstrumentRequest>();
        grpcRequest.Id = id;
        await _client.UpdateInstrumentAsync(grpcRequest);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        var grpcRequest = new DeleteInstrumentRequest { Id = id };
        await _client.DeleteInstrumentAsync(grpcRequest);
        return NoContent();
    }
}
