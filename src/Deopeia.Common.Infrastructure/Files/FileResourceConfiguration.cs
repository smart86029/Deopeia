using Deopeia.Common.Domain.Files;

namespace Deopeia.Common.Infrastructure.Files;

public class FileResourceConfiguration : IEntityTypeConfiguration<FileResource>
{
    public void Configure(EntityTypeBuilder<FileResource> builder)
    {
        builder.HasDiscriminator(x => x.Type).HasValue<Image>(FileResourceType.Image);

        builder.Ignore(x => x.Content);

        builder.Ignore(x => x.PresignedUri);

        builder.HasIndex(x => x.Type);
    }
}
