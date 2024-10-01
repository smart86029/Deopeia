using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts;

internal class FuturesConfiguration : IEntityTypeConfiguration<FuturesContract>
{
    public void Configure(EntityTypeBuilder<FuturesContract> builder)
    {
        builder.HasIndex(x => x.ContractSpecificationId);
    }
}
