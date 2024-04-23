using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Auditing;
using Viriplaca.Common.Data.Comparers;

namespace Viriplaca.Common.Data.Auditing;

public class DataAccessAuditTrailConfiguration : IEntityTypeConfiguration<DataAccessAuditTrail>
{
    public void Configure(EntityTypeBuilder<DataAccessAuditTrail> builder)
    {
        builder
            .Property(x => x.Keys)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object>>>(new DictionaryComparer<string, object>());

        builder
            .Property(x => x.OldValues)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object?>>>(new DictionaryComparer<string, object?>());

        builder
            .Property(x => x.NewValues)
            .HasConversion<JsonConverter<IReadOnlyDictionary<string, object?>>>(new DictionaryComparer<string, object?>());

        builder
            .Property(x => x.PropertyNames)
            .HasConversion<JsonConverter<IReadOnlyCollection<string>>>(new EnumerableComparer<string>());
    }
}
