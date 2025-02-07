using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ComplexProperty(
            x => x.Money,
            x =>
            {
                x.Ignore(nameof(Transaction.Money.CurrencyCode));
                x.Property(y => y.Amount)
                    .HasColumnName(nameof(Transaction.Money.Amount).ToSnakeCaseLower());
            }
        );
    }
}
