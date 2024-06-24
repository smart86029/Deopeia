using Deopeia.Common.Localization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deopeia.Common.Infrastructure.Localization;

public class LocaleResourceConfiguration : IEntityTypeConfiguration<LocaleResource>
{
    public void Configure(EntityTypeBuilder<LocaleResource> builder)
    {
        builder.Property(x => x.Code).IsRequired().HasMaxLength(128);

        builder.Property(x => x.Content).IsRequired().HasMaxLength(1024);

        builder.ToTable(nameof(LocaleResource), "Common");

        builder.HasKey(x => new
        {
            x.Culture,
            x.Type,
            x.Code
        });
    }
}
