using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Files;

namespace Viriplaca.Common.Data.Configurations;

public class ImageConfiguration : EntityConfiguration<Image>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
    }
}
