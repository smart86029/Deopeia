using Viriplaca.Common.Files;
using Viriplaca.HR.App.Employees.GetEmployee;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data.Employees.GetEmployee;

public class GetEmployeeQueryHandler(SqlConnection connection, IImageRepository imageRepository)
    : IRequestHandler<GetEmployeeQuery, EmployeeDto>
{
    private readonly SqlConnection _connection = connection;
    private readonly IImageRepository _imageRepository = imageRepository;

    public async Task<EmployeeDto> Handle(
        GetEmployeeQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql =
            @"
SELECT
    Id,
    FirstName,
    LastName,
    BirthDate,
    Sex,
    MaritalStatus,
    UserId,
    AvatarId,
    DepartmentId,
    JobId
FROM HR.Person
WHERE Id = @Id AND Type = @Employee
";
        var param = new { request.Id, PersonType.Employee, };
        var result = await _connection.QueryFirstAsync<EmployeeDto>(sql, param);
        if (result.AvatarId.HasValue)
        {
            var image = await _imageRepository.GetImageAsync(result.AvatarId.Value);
            result.AvatarUrl = image.PresignedUri?.ToString();
        }

        return result;
    }
}
