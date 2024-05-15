using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.App.Employees.CreateEmployee;

public record CreateEmployeeCommand(
    string FirstName,
    string? LastName,
    DateOnly BirthDate,
    Sex Sex,
    MaritalStatus MaritalStatus,
    Guid? AvatarId,
    Guid DepartmentId,
    Guid Job
) : IRequest<Guid> { }
