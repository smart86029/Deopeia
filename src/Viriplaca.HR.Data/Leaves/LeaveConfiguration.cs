using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.Data.Leaves;

internal class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.HasIndex(x => x.EmployeeId);
    }
}
