namespace Deopeia.Common.Infrastructure.Localization;

public class LocaleResourceConfiguration : IEntityTypeConfiguration<LocaleResource>
{
    public void Configure(EntityTypeBuilder<LocaleResource> builder)
    {
        builder.HasKey(x => new
        {
            x.Culture,
            x.Type,
            x.Code,
        });
    }
}
