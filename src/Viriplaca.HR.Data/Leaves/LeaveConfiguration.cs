using Viriplaca.HR.Domain.Leaves;

namespace Viriplaca.HR.Data.Leaves;

internal class LeaveConfiguration : EntityConfiguration<Leave>
{
    public override void Configure(EntityTypeBuilder<Leave> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.EmployeeId);
    }
}
