using Deopeia.Common.Domain.Auditing;
using Deopeia.Common.Infrastructure.Comparers;

namespace Deopeia.Common.Infrastructure.Auditing;

public class DataAccessAuditTrailConfiguration : IEntityTypeConfiguration<DataAccessAuditTrail>
{
    public void Configure(EntityTypeBuilder<DataAccessAuditTrail> builder)
    {
        builder
            .Property(x => x.Keys)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object>>>(
                new DictionaryComparer<string, object>()
            )
            .HasColumnType("jsonb");

        builder
            .Property(x => x.OldValues)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object?>>>(
                new DictionaryComparer<string, object?>()
            )
            .HasColumnType("jsonb");

        builder
            .Property(x => x.NewValues)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object?>>>(
                new DictionaryComparer<string, object?>()
            )
            .HasColumnType("jsonb");

        builder
            .Property(x => x.PropertyNames)
            .HasConversion<JsonConverter<IReadOnlyCollection<string>>>(
                new EnumerableComparer<string>()
            )
            .HasColumnType("jsonb");
    }
}
