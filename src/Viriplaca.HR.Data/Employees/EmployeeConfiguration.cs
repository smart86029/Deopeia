using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.Data.NewFolder;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(x => x.DepartmentId);

        builder.HasIndex(x => x.JobId);
    }
}
