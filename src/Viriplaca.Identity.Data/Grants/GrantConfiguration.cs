using Viriplaca.Identity.Domain.Grants;
using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;
using Viriplaca.Identity.Domain.Grants.RefreshTokens;

namespace Viriplaca.Identity.Data.Grants;

internal class GrantConfiguration : EntityConfiguration<Grant>
{
    public override void Configure(EntityTypeBuilder<Grant> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<AuthorizationCode>(GrantTypes.AuthorizationCode)
            .HasValue<RefreshToken>(GrantTypes.RefreshToken);

        builder
            .Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion<StringReadOnlyCollectionConverter>(new EnumerableComparer<string>());

        builder
            .HasIndex(x => x.Key)
            .IsUnique();

        builder.HasIndex(x => x.ClientId);
    }
}
