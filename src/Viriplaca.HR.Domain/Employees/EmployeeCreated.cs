namespace Viriplaca.HR.Domain.Employees;

public record EmployeeCreated(Guid EmployeeId)
    : DomainEvent
{
}
