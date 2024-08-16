using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Infrastructure.Companies;

internal class CompanyLocaleConfiguration
    : EntityLocaleConfiguration<Company, CompanyLocale, CompanyId>
{
    public override void Configure(EntityTypeBuilder<CompanyLocale> builder)
    {
        base.Configure(builder);
    }
}
