using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class FuturesConfiguration : IEntityTypeConfiguration<FuturesContract>
{
    public void Configure(EntityTypeBuilder<FuturesContract> builder)
    {
        builder.ComplexProperty(x => x.ContractSize);
    }
}
