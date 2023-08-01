using Domain.Assessments;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
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
            .HasConversion(id => id.Value, id => new SummativeAssessmentId(id));

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(fa => fa.TeacherId);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(fa => fa.SubjectId);

        builder.OwnsOne(sa => sa.StudentId, sib =>
        {
            sib.WithOwner().HasForeignKey("SummativeAssessmentId");
        });

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();

    }
}

public class SummativeAssessmentResultConfiguration : IEntityTypeConfiguration<SummativeAssessmentResult>
{
    public void Configure(EntityTypeBuilder<SummativeAssessmentResult> builder)
    {
        builder.ToTable("summative_assessment_results");

        builder.Property<int>("Id");

        builder.HasKey("Id");

        builder.OwnsOne(sa => sa.Grade, gb =>
        {
            gb.Property(g => g.Grade).HasConversion<string>();
            gb.Property(g => g.Percentage);
        });
    }
}
