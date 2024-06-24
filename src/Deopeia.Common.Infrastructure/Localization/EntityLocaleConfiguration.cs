using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deopeia.Common.Infrastructure.Localization;

public abstract class EntityLocaleConfiguration<TEntity, TLocale>
    : IEntityTypeConfiguration<TLocale>
    where TEntity : Entity, ILocalizable<TLocale>
    where TLocale : EntityLocale
{
    public virtual void Configure(EntityTypeBuilder<TLocale> builder)
    {
        builder.Property(x => x.EntityId).HasColumnName($"{typeof(TEntity).Name}Id");

        builder.Property(x => x.Culture).IsRequired().HasMaxLength(16);

        builder.HasKey(x => new { x.EntityId, x.Culture });

        builder.HasOne<TEntity>().WithMany(x => x.Locales).HasForeignKey(x => x.EntityId);
    }
}
