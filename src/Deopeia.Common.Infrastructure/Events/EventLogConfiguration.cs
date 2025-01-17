using Deopeia.Common.Events;

namespace Deopeia.Common.Infrastructure.Events;

internal class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> builder)
    {
        builder.HasKey(x => x.EventId);

        builder.Ignore(x => x.Event);
    }
}
