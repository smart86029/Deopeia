using Deopeia.Common.Domain.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deopeia.Common.Infrastructure.Auditing;

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
