using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Files;

namespace Viriplaca.Common.Data.Configurations;

public class FileResourceConfiguration : EntityConfiguration<FileResource>
{
    public override void Configure(EntityTypeBuilder<FileResource> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<Image>(FileResourceType.Image);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.Extension)
            .IsRequired()
            .HasMaxLength(16);

        builder.Ignore(x => x.Content);
        builder.Ignore(x => x.PresignedUri);

        builder.ToTable(nameof(FileResource), "Common");

        builder.HasIndex(x => x.Type);
    }
}
