using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;
using Deopeia.Identity.Domain.Grants.RefreshTokens;

namespace Deopeia.Identity.Infrastructure.Grants;

internal class GrantConfiguration : IEntityTypeConfiguration<Grant>
{
    public void Configure(EntityTypeBuilder<Grant> builder)
    {
        builder
            .HasDiscriminator(x => x.Type)
            .HasValue<AuthorizationCode>(GrantTypes.AuthorizationCode)
            .HasValue<RefreshToken>(GrantTypes.RefreshToken);

        builder.Property(x => x.Key);

        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion<JsonConverter<IReadOnlyCollection<string>>>(
                new EnumerableComparer<string>()
            );

        builder.HasIndex(x => x.Key).IsUnique();

        builder.HasIndex(x => x.ClientId);
    }
}
