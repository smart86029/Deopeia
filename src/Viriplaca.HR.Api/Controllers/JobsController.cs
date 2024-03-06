using Viriplaca.HR.App.Jobs.GetJobOptions;
using Viriplaca.HR.App.Jobs.GetJobs;

namespace Viriplaca.HR.Api.Controllers;

public class JobsController : ApiController<JobsController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetJobOptions([FromQuery] GetJobOptionsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs([FromQuery] GetJobsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
