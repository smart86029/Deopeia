using Viriplaca.HR.Domain.Departments;

namespace Viriplaca.HR.App.Departments.UpdateDepartment;

public class UpdateDepartmentCommandHandler(
    IHRUnitOfWork unitOfWork,
    IDepartmentRepository departmentRepository
) : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentAsync(request.Id);
        department.UpdateName(request.Name);
        if (department.IsEnabled)
        {
            department.Enable();
        }
        else
        {
            department.Disable();
        }

        _departmentRepository.Update(department);
        await _unitOfWork.CommitAsync();
    }
}
