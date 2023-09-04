using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Assessments;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;
public class SummativeAssessmentConfiguration : IEntityTypeConfiguration<SummativeAssessment>
{
    public void Configure(EntityTypeBuilder<SummativeAssessment> builder)
    {
        builder.HasBaseType(typeof(Assessment));

        builder.Property(a => a.YearLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(sa => sa.PlanningNotes)
            .HasMaxLength(500);

    }
}

public class SummativeAssessmentResultConfiguration : IEntityTypeConfiguration<SummativeAssessmentResult>
{
    public void Configure(EntityTypeBuilder<SummativeAssessmentResult> builder)
    {
        builder.ToTable("summative_assessment_results");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.OwnsOne(sa => sa.Grade, gb =>
        {
            gb.Property(g => g.Grade)
            .HasConversion<string>()
            .HasMaxLength(10);

            gb.Property(g => g.Percentage);
        });
    }
}
