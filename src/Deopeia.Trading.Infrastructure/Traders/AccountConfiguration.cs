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
                x.Property(y => y.Amount).HasColumnName(nameof(Account.Balance).ToLower());
            }
        );

        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.TraderId, x.CurrencyCode });
    }
}
