using Domain.Assessments;
using Domain.Assessments.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class FormativeAssessmentConfiguration : IEntityTypeConfiguration<FormativeAssessment>
{
    public void Configure(EntityTypeBuilder<FormativeAssessment> builder)
    {
        builder.ToTable("formative_assessments");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => AssessmentId.Create(id));

        builder.Property(a => a.TeacherId)
            .HasConversion(id => id.Value, id => TeacherIdForReference.Create(id));

        builder.Property(a => a.SubjectId)
            .HasConversion(id => id.Value, id => SubjectIdForReference.Create(id));

        builder.Property(a => a.StudentId)
            .HasConversion(id => id.Value, id => StudentIdForReference.Create(id));

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();

    }
}
