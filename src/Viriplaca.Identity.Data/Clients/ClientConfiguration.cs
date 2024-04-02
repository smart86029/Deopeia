using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.Data.Clients;

internal class ClientConfiguration : EntityConfiguration<Client>
{
    public override void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .Property(x => x.Secret)
            .HasMaxLength(32);

        builder
            .Property(x => x.Scopes)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion<StringReadOnlyCollectionConverter>(new EnumerableComparer<string>());

        builder
            .Property(x => x.RedirectUris)
            .IsRequired()
            .HasMaxLength(1024)
            .HasConversion<UriReadOnlyCollectionConverter>(new EnumerableComparer<Uri>());
    }
}
