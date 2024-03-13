namespace Viriplaca.HR.App.Departments.GetDepartment;

public record GetDepartmentQuery(Guid Id)
    : IRequest<DepartmentDto>
{
}
