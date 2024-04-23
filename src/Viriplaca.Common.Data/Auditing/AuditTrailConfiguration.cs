using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Auditing;

namespace Viriplaca.Common.Data.Auditing;

public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<DataAccessAuditTrail>(AuditTrailType.DataAccess);

        builder.ToTable(nameof(AuditTrail), "Common");
    }
}
