using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Infrastructure.Accounts;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ComplexProperty(x => x.Balance);

        builder.ComplexProperty(x => x.Margin);
    }
}
