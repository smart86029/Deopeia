namespace Viriplaca.HR.App.Departments.GetDepartments;

public record GetDepartmentsQuery(bool? IsEnabled)
    : PageQuery<DepartmentDto>
{
}
