using Viriplaca.HR.Domain.Departments;

namespace Viriplaca.HR.Data.Departments;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.HasIndex(x => x.ParentId);
    }
}
