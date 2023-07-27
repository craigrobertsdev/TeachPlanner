using Domain.Assessments;
using Domain.Assessments.Entities;
using Domain.Assessments.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class SummativeAssessmentConfiguration : BaseAssessmentConfiguration<SummativeAssessment>
{
    public override void Configure(EntityTypeBuilder<SummativeAssessment> builder)
    {
        base.Configure(builder);

        ConfigureSummativeAssessment(builder);

    }

    private void ConfigureSummativeAssessment(EntityTypeBuilder<SummativeAssessment> builder)
    {
        builder.ToTable("SummativeAssessments");

        builder.HasOne(sa => sa.Result);

    }
}

public class SummativeAssessmentResultConfiguration : IEntityTypeConfiguration<SummativeAssessmentResult>
{
    public void Configure(EntityTypeBuilder<SummativeAssessmentResult> builder)
    {
        builder.ToTable("SummativeAssessmentResults");

        builder.HasKey("Id");

        builder.Property(rb => rb.Id)
            .HasColumnName("SummativeAssessmentResultId")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new SummativeAssessmentResultId(value));

        builder.Property(rb => rb.StudentId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new StudentId(value));

        builder.Property(rb => rb.SubjectId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new SubjectId(value));

        builder.OwnsOne(rb => rb.Grade)
            .Property(g => g.Grade)
            .HasConversion<string>();

        builder.OwnsOne(rb => rb.Grade)
            .Property(g => g.Id)
            .HasConversion(
                id => id.Value,
                value => new GradeId(value));


    }
}
