using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Infrastructure.Companies;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder) { }
}
