using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(x => x.Code).IsRequired().HasMaxLength(32);

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
