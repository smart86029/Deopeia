using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

namespace Viriplaca.Identity.Data.Grants.AuthorizationCodes;

internal class AuthorizationCodeConfiguration : EntityConfiguration<AuthorizationCode>
{
    public override void Configure(EntityTypeBuilder<AuthorizationCode> builder)
    {
        builder
            .Property(x => x.RedirectUri)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion<StringReadOnlyCollectionConverter>(new EnumerableComparer<string>());

        builder
            .Property(x => x.CodeChallenge)
            .IsRequired()
            .HasMaxLength(64);

        builder
            .Property(x => x.CodeChallengeMethod)
            .IsRequired()
            .HasMaxLength(16);
    }
}
