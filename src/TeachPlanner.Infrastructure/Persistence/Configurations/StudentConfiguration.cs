using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Students;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("students");

        builder.Property(s => s.Id)
            .HasColumnName("Id");

        builder.HasMany(s => s.Reports)
            .WithOne()
            .IsRequired();

        builder.HasMany(s => s.Assessments)
            .WithOne()
            .IsRequired();
    }
}
