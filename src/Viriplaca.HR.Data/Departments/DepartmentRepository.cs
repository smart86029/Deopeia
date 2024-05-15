using Viriplaca.HR.Domain.Departments;

namespace Viriplaca.HR.Data.Departments;

public class DepartmentRepository(HRContext context) : IDepartmentRepository
{
    private readonly DbSet<Department> _departments = context.Set<Department>();

    public async Task<ICollection<Department>> GetDepartmentsAsync()
    {
        var results = await _departments.ToListAsync();

        return results;
    }

    public async Task<Department> GetDepartmentAsync(Guid departmentId)
    {
        var result =
            await _departments.SingleOrDefaultAsync(x => x.Id == departmentId)
            ?? throw new Exception(departmentId.ToString());

        return result;
    }

    public async Task<int> GetCountAsync(Guid parentId)
    {
        var result = await _departments.CountAsync(x => x.ParentId == parentId);

        return result;
    }

    public void Add(Department department)
    {
        _departments.Add(department);
    }

    public void Update(Department department)
    {
        _departments.Update(department);
    }

    public void Remove(Department department)
    {
        _departments.Remove(department);
    }
}
