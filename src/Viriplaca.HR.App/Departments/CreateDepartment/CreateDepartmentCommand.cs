namespace Viriplaca.HR.App.Departments.CreateDepartment;

public record CreateDepartmentCommand(
    string Name,
    bool IsEnabled,
    Guid? ParentId)
    : IRequest<Guid>
{
}
