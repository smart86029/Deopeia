using Microsoft.AspNetCore.Mvc;
using Viriplaca.HR.App.Departments.GetDepartments;

namespace Viriplaca.HR.Api.Controllers;

public class DepartmentsController : ApiController<DepartmentsController>
{
    [HttpGet]
    public async Task<IActionResult> GetDepartments([FromQuery] GetDepartmentsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
