using Viriplaca.HR.Domain.Employees;

namespace Viriplaca.HR.Data.Employees;

internal class JobChangeConfiguration : EntityConfiguration<JobChange>
{
    public override void Configure(EntityTypeBuilder<JobChange> builder)
    {
        builder.HasIndex(x => x.DepartmentId);

        builder.HasIndex(x => x.JobId);
    }
}
