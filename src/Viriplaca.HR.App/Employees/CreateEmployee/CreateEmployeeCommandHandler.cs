using Viriplaca.Common.Files;
using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.App.Employees.CreateEmployee;

public class CreateEmployeeCommandHandler(
    IHRUnitOfWork unitOfWork,
    IEmployeeRepository employeeRepository,
    IImageRepository imageRepository)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IHRUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Sex,
            request.MaritalStatus);

        if (request.AvatarId.HasValue)
        {
            var image = await _imageRepository.GetImageAsync(request.AvatarId.Value);
            employee.UpdateAvatar(image);
        }

        _employeeRepository.Add(employee);
        await _unitOfWork.CommitAsync();

        return employee.Id;
    }
}
