using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.Configurations;

internal class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("Id");

        builder.Property(a => a.YearLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(sa => sa.PlanningNotes)
            .HasMaxLength(500);

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

public class SummativeAssessmentResultConfiguration : IEntityTypeConfiguration<AssessmentResult>
{
    public void Configure(EntityTypeBuilder<AssessmentResult> builder)
    {
        builder.ToTable("assessment_results");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.Property(ar => ar.Comments)
            .HasMaxLength(500);

        builder.OwnsOne(sa => sa.Grade, gb =>
        {
            gb.Property(g => g.Grade)
            .HasConversion<string>()
            .HasMaxLength(10);

            gb.Property(g => g.Percentage);
        });
    }
}
