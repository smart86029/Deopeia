using Viriplaca.Common.Data.Localization;
using Viriplaca.HR.Domain.LeaveTypes;

namespace Viriplaca.Identity.Data.Permissions;

internal class LeaveTypeLocaleConfiguration : EntityLocaleConfiguration<LeaveType, LeaveTypeLocale>
{
    public override void Configure(EntityTypeBuilder<LeaveTypeLocale> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}
