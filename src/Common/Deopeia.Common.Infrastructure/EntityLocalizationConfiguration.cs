namespace Deopeia.Common.Infrastructure;

public abstract class EntityLocalizationConfiguration<TEntity, TLocalization, TEntityId>
    : IEntityTypeConfiguration<TLocalization>
    where TEntity : Entity<TEntityId>, ILocalizable<TLocalization, TEntityId>
    where TLocalization : EntityLocalization<TEntityId>
    where TEntityId : struct, IEntityId
{
    public virtual void Configure(EntityTypeBuilder<TLocalization> builder)
    {
        builder
            .Property(x => x.EntityId)
            .HasColumnName($"{typeof(TEntity).Name.ToSnakeCaseLower()}_id");

        builder.HasKey(x => new { x.EntityId, x.Culture });

        builder.HasOne<TEntity>().WithMany(x => x.Localizations).HasForeignKey(x => x.EntityId);
    }
}
