using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.App.Employees.UpdateEmployee;

public record UpdateEmployeeCommand(
    Guid Id,
    string FirstName,
    string? LastName,
    DateOnly BirthDate,
    Sex Sex,
    MaritalStatus MaritalStatus,
    Guid? AvatarId,
    Guid DepartmentId,
    Guid Job
) : IRequest { }
