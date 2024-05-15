using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Files;

namespace Viriplaca.Common.Data.Files;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder) { }
}
