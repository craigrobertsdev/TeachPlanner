using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

internal class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("Id");

        builder.HasDiscriminator<string>("assessment_type")
            .HasValue<FormativeAssessment>("formative")
            .HasValue<SummativeAssessment>("summative");

        builder.Property("assessment_type")
            .HasMaxLength(50);

        builder.HasOne<Subject>()
            .WithMany()
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany(s => s.Assessments)
            .HasForeignKey(a => a.StudentId)
            .IsRequired();

        builder.Property(a => a.YearLevel)
            .HasConversion<string>()
            .HasMaxLength(15);
    }
}
