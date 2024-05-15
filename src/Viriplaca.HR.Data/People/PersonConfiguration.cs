using Viriplaca.HR.Domain.Employees;
using Viriplaca.HR.Domain.People;

namespace Viriplaca.HR.Data.People;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasDiscriminator(x => x.Type).HasValue<Employee>(PersonType.Employee);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(32);

        builder.Property(x => x.LastName).HasMaxLength(32);

        builder.HasIndex(x => x.UserId);
    }
}
