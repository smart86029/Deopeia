using Viriplaca.HR.Domain.Departments;

namespace Viriplaca.HR.App.Departments.CreateDepartment;

public class CreateDepartmentCommandHandler(
    IHRUnitOfWork unitOfWork,
    IDepartmentRepository departmentRepository
) : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;

    public async Task<Guid> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken
    )
    {
        var department = new Department(request.Name, request.IsEnabled, request.ParentId);

        _departmentRepository.Add(department);
        await _unitOfWork.CommitAsync();

        return department.Id;
    }
}
