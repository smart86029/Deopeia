using Deopeia.Quote.Application.ContractSpecifications.CreateContractSpecification;
using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecification;
using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecificationOptions;
using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecifications;
using Deopeia.Quote.Application.ContractSpecifications.UpdateContractSpecification;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class ContractSpecificationsController : ApiController<ContractSpecificationsController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions(
        [FromQuery] GetContractSpecificationOptionsQuery query
    )
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetContractSpecificationsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetContractSpecificationQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractSpecificationCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{mic}")]
    public async Task<IActionResult> Update(
        [FromRoute] string mic,
        [FromBody] UpdateContractSpecificationCommand command
    )
    {
        command = command with { Mic = mic };
        await Sender.Send(command);

        return NoContent();
    }
}
