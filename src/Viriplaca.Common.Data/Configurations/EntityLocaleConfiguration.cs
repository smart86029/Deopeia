using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viriplaca.Common.Data.Converters;

namespace Viriplaca.Common.Data.Configurations;

public abstract class EntityLocaleConfiguration<TEntity, TLocale> : IEntityTypeConfiguration<TLocale>
    where TEntity : Entity, ILocalizable<TLocale>
    where TLocale : EntityLocale
{
    public virtual void Configure(EntityTypeBuilder<TLocale> builder)
    {
        builder
            .Property(x => x.EntityId)
            .HasColumnName($"{typeof(TEntity).Name}Id");

        builder
            .Property(x => x.Culture)
            .HasColumnName("LocaleCode")
            .HasConversion<CultureInfoConverter>()
            .IsRequired()
            .HasMaxLength(16);

        builder.HasKey(x => new { x.EntityId, x.Culture });

        builder
            .HasOne<TEntity>()
            .WithMany(x => x.Locales)
            .HasForeignKey(x => x.EntityId);
    }
}
