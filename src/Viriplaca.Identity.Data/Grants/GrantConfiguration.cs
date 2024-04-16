using Viriplaca.Identity.Domain.Grants;
using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

namespace Viriplaca.Identity.Data.Grants;

internal class GrantConfiguration : EntityConfiguration<Grant>
{
    public override void Configure(EntityTypeBuilder<Grant> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<AuthorizationCode>(GrantTypes.AuthorizationCode);

        builder
            .Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .HasIndex(x => x.Key)
            .IsUnique();

        builder.HasIndex(x => x.ClientId);
    }
}
