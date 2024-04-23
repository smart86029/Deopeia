using Viriplaca.HR.Domain.Jobs;

namespace Viriplaca.HR.Data.Jobs;

internal class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder
           .Property(x => x.Title)
           .IsRequired()
           .HasMaxLength(32);
    }
}
