using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal sealed class AuthenticatorConfiguration : IEntityTypeConfiguration<Authenticator>
{
    private const string Key = "63D203DF364FD2FB2293B496E6C92CD4";

    public void Configure(EntityTypeBuilder<Authenticator> builder)
    {
        builder.Property(x => x.Id).HasColumnName(nameof(UserId).ToSnakeCaseLower());

        builder
            .Property(x => x.SecretKey)
            .HasColumnName("SecretKeyCiphertext".ToSnakeCaseLower())
            .HasConversion(
                plaintext =>
                    plaintext.IsNullOrWhiteSpace() ? string.Empty : plaintext.AesEncrypt(Key),
                ciphertext =>
                    ciphertext.IsNullOrWhiteSpace() ? string.Empty : ciphertext.AesDecrypt(Key)
            );
    }
}
