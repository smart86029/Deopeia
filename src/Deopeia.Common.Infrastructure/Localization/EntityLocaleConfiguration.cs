namespace Deopeia.Common.Infrastructure.Localization;

public abstract class EntityLocaleConfiguration<TEntity, TLocale, TEntityId>
    : IEntityTypeConfiguration<TLocale>
    where TEntity : Entity<TEntityId>, ILocalizable<TLocale, TEntityId>
    where TLocale : EntityLocale<TEntityId>
    where TEntityId : struct, IEntityId
{
    public virtual void Configure(EntityTypeBuilder<TLocale> builder)
    {
        builder.Property(x => x.EntityId).HasColumnName($"{typeof(TEntity).Name}Id");

        builder.HasKey(x => new { x.EntityId, x.Culture });

        builder.HasOne<TEntity>().WithMany(x => x.Locales).HasForeignKey(x => x.EntityId);
    }
}
