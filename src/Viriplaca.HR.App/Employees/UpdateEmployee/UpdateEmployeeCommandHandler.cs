using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.App.Employees.UpdateEmployee;

public class UpdateEmployeeCommandHandler(IHRUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
    : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeAsync(request.Id);
        employee.UpdateFirstName(request.FirstName);
        employee.UpdateLastName(request.LastName);
        employee.UpdateBirthDate(request.BirthDate);
        employee.UpdateSex(request.Sex);
        employee.UpdateMaritalStatus(request.MaritalStatus);

        _employeeRepository.Update(employee);
        await _unitOfWork.CommitAsync();
    }
}
