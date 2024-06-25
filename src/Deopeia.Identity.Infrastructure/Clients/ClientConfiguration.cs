using Deopeia.Identity.Domain.Clients;

namespace Deopeia.Identity.Infrastructure.Clients;

internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(32);

        builder.Property(x => x.Secret).HasMaxLength(32);

        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion<JsonConverter<IReadOnlyCollection<string>>>(
                new EnumerableComparer<string>()
            );

        builder
            .Property(x => x.RedirectUris)
            .IsRequired()
            .HasMaxLength(1024)
            .HasConversion<JsonConverter<IReadOnlyCollection<Uri>>>(new EnumerableComparer<Uri>());
    }
}
