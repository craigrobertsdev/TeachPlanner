using Domain.Assessments;
using Domain.Assessments.Entities;
using Domain.Assessments.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class SummativeAssessmentConfiguration : IEntityTypeConfiguration<SummativeAssessment>
{
    public void Configure(EntityTypeBuilder<SummativeAssessment> builder)
    {
        builder.ToTable("summative_assessments");

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

        builder.HasOne(sa => sa.Result);
    }
}

public class SummativeAssessmentResultConfiguration : IEntityTypeConfiguration<SummativeAssessmentResult>
{
    public void Configure(EntityTypeBuilder<SummativeAssessmentResult> builder)
    {
        builder.ToTable("summative_assessment_results");

        builder.HasKey("Id");

        builder.Property(rb => rb.Id)
            .HasColumnName("SummativeAssessmentResultId")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SummativeAssessmentResultId.Create(value));

        builder.Property(rb => rb.StudentId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => StudentId.Create(value));

        builder.Property(rb => rb.SubjectId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SubjectId.Create(value));

        builder.OwnsOne(rb => rb.Grade)
            .Property(g => g.Grade)
            .HasConversion<string>();

        builder.OwnsOne(rb => rb.Grade)
            .Property(g => g.Id)
            .HasConversion(
                id => id.Value,
                value => GradeId.Create(value));
    }
}
