using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.App.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler(IHRUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Sex,
            request.MaritalStatus);

        _employeeRepository.Add(employee);
        await _unitOfWork.CommitAsync();

        return employee.Id;
    }
}
