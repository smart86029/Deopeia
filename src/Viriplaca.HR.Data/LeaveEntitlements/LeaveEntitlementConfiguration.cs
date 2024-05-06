using Viriplaca.HR.Domain.LeaveEntitlements;

namespace Viriplaca.HR.Data.LeaveEntitlements;

internal class LeaveEntitlementConfiguration : IEntityTypeConfiguration<LeaveEntitlement>
{
    public void Configure(EntityTypeBuilder<LeaveEntitlement> builder)
    {
        builder
            .HasIndex(x => new { x.EmployeeId, x.StartedOn, x.EndedOn, x.Type })
            .IsUnique();
    }
}
