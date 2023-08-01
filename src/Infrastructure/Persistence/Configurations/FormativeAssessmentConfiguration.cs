using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class FormativeAssessmentConfiguration : IEntityTypeConfiguration<FormativeAssessment>
{
    public void Configure(EntityTypeBuilder<FormativeAssessment> builder)
    {
        builder.ToTable("formative_assessments");

        builder.HasKey(fa => fa.Id);

        builder.Property(fa => fa.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new FormativeAssessmentId(id));

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(fa => fa.TeacherId);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(fa => fa.SubjectId);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(fa => fa.StudentId);

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();
    }
}
