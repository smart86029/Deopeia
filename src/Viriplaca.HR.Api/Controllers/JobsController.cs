using Viriplaca.HR.App.Jobs.CreateJob;
using Viriplaca.HR.App.Jobs.GetJob;
using Viriplaca.HR.App.Jobs.GetJobOptions;
using Viriplaca.HR.App.Jobs.GetJobs;
using Viriplaca.HR.App.Jobs.UpdateJob;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob([FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetJobQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var result = await Sender.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(
        [FromRoute] Guid id,
        [FromBody] UpdateJobCommand command
    )
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);

        return Ok();
    }
}
