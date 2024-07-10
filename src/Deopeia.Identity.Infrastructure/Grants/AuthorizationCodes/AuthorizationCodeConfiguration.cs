using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Infrastructure.Grants.AuthorizationCodes;

internal class AuthorizationCodeConfiguration : IEntityTypeConfiguration<AuthorizationCode>
{
    public void Configure(EntityTypeBuilder<AuthorizationCode> builder) { }
}
