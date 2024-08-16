using Deopeia.Common.Domain.Auditing;

namespace Deopeia.Common.Infrastructure.Auditing;

public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<DataAccessAuditTrail>(AuditTrailType.DataAccess);
    }
}
