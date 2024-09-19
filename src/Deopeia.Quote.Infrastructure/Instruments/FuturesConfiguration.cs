using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class FuturesConfiguration : IEntityTypeConfiguration<Futures>
{
    public void Configure(EntityTypeBuilder<Futures> builder)
    {
        builder.ComplexProperty(x => x.ContractSize);
    }
}
