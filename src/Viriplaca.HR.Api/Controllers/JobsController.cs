using Microsoft.AspNetCore.Mvc;
using Viriplaca.HR.App.Jobs.GetJobs;

namespace Viriplaca.HR.Api.Controllers;

public class JobsController : ApiController<JobsController>
{
    [HttpGet]
    public async Task<IActionResult> GetJobs([FromQuery] GetJobsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
