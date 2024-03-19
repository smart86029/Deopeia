using Viriplaca.HR.App.Images.UploadImage;

namespace Viriplaca.HR.Api.Controllers;

public class ImagesController : ApiController<ImagesController>
{
    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageCommand command)
    {
        var result = await Sender.Send(command);

        return Ok(result);
    }
}
