using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Student;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teacher;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

internal class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("Id");

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(fa => fa.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(fa => fa.SubjectId)
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(fa => fa.StudentId)
            .IsRequired();

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();
    }
}
