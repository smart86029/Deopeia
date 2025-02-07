using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ComplexProperty(
            x => x.Balance,
            x =>
            {
                x.Ignore(nameof(Account.Balance.CurrencyCode));
                x.Property(y => y.Amount).HasColumnName(nameof(Account.Balance).ToSnakeCaseLower());
            }
        );

        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.TraderId, x.CurrencyCode });

        builder
            .HasMany(x => x.Transactions)
            .WithOne()
            .HasForeignKey(x => new { x.TraderId, x.CurrencyCode });
    }
}
