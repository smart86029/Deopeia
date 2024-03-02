using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.Data.NewFolder;

internal class EmployeeConfiguration : EntityConfiguration<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasIndex(x => x.DepartmentId);

        builder.HasIndex(x => x.JobId);
    }
}
