using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.App.Employees.GetEmployee;

public record GetEmployeeQuery(Guid id)
    : IRequest<EmployeeDto>
{
}
