using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.Data.Employees;

public class EmployeeRepository(HRContext context)
    : IEmployeeRepository
{
    private readonly DbSet<Employee> _employees = context.Set<Employee>();

    public async Task<ICollection<Employee>> GetEmployeesAsync(int offset, int limit)
    {
        var results = await _employees
            .Include(x => x.JobChanges)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return results;
    }

    public async Task<ICollection<Employee>> GetEmployeesAsync(Guid departmentId, int offset, int limit)
    {
        var results = await _employees
            .Where(x => x.DepartmentId == departmentId)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return results;
    }

    public async Task<Employee> GetEmployeeAsync(Guid employeeId)
    {
        var result = await _employees
            .Include(x => x.JobChanges)
            .SingleOrDefaultAsync(x => x.Id == employeeId)
            ?? throw new Exception(employeeId.ToString());

        return result;
    }

    public async Task<int> GetCountAsync()
    {
        var result = await _employees
            .CountAsync();

        return result;
    }

    public async Task<int> GetCountByUserAsync(Guid userId)
    {
        var result = await _employees
            .CountAsync(x => x.UserId == userId);

        return result;
    }

    public async Task<int> GetCountByDepartmentAsync(Guid departmentId)
    {
        var result = await _employees
            .CountAsync(x => x.DepartmentId == departmentId);

        return result;
    }

    public async Task<int> GetCountByJobAsync(Guid jobId)
    {
        var result = await _employees
            .CountAsync(x => x.JobId == jobId);

        return result;
    }

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    public void Update(Employee employee)
    {
        _employees.Update(employee);
    }
}
