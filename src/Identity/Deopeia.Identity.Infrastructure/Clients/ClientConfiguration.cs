using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Infrastructure.Clients;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasConversion<JsonConverter<IReadOnlyCollection<string>>>(
                new EnumerableComparer<string>()
            )
            .HasColumnType("jsonb");

        builder
            .Property(x => x.RedirectUris)
            .IsRequired()
            .HasConversion<JsonConverter<IReadOnlyCollection<Uri>>>(new EnumerableComparer<Uri>())
            .HasColumnType("jsonb");
    }
}
