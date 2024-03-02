using Viriplaca.HR.Domain.Jobs;

namespace Viriplaca.HR.Data.Jobs;

internal class JobConfiguration : EntityConfiguration<Job>
{
    public override void Configure(EntityTypeBuilder<Job> builder)
    {
        builder
           .Property(x => x.Title)
           .IsRequired()
           .HasMaxLength(32);
    }
}
