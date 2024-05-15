using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Viriplaca.Common.EntityFrameworkCore.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Ignore(x => x.DomainEvents);
    }
}
