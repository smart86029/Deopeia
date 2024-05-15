namespace Viriplaca.HR.App.Departments.UpdateDepartment;

public record UpdateDepartmentCommand(Guid Id, string Name, bool IsEnabled, Guid? ParentId)
    : IRequest { }
