using Domain.Assessments;
using Domain.ReportAggregate;
using Domain.StudentAggregate;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(s => s.Value, value => new StudentId(value))
            .ValueGeneratedNever();

        builder.HasMany<Report>()
            .WithOne()
            .HasForeignKey(r => r.StudentId)
            .IsRequired();

        builder.HasMany<SummativeAssessment>()
            .WithOne()
            .HasForeignKey(s => s.StudentId)
            .IsRequired();

        builder.HasMany<FormativeAssessment>()
            .WithOne()
            .HasForeignKey(s => s.StudentId)
            .IsRequired();

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(s => s.TeacherId);

        builder.Navigation(s => s.ReportIds).Metadata.SetField("_reportIds");
        builder.Navigation(s => s.ReportIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(s => s.SummativeAssessmentIds).Metadata.SetField("_summativeAssessmentIds");
        builder.Navigation(s => s.SummativeAssessmentIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(s => s.FormativeAssessmentIds).Metadata.SetField("_formativeAssessmentIds");
        builder.Navigation(s => s.FormativeAssessmentIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
