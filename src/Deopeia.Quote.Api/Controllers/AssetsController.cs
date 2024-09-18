using Deopeia.Quote.Application.Assets.CreateAsset;
using Deopeia.Quote.Application.Assets.GetAsset;
using Deopeia.Quote.Application.Assets.GetAssets;
using Deopeia.Quote.Application.Assets.UpdateAsset;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class AssetsController : ApiController<AssetsController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAssetsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetAssetQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssetCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateAssetCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
