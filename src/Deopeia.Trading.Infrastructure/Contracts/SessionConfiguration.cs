using Deopeia.Trading.Domain.Contracts;

namespace Deopeia.Trading.Infrastructure.Contracts;

internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => new { x.Symbol, x.DayOfWeek });
    }
}
