using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.Assessments;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Database.Configurations;

internal class AssessmentConfiguration : IEntityTypeConfiguration<Assessment> {
    public void Configure(EntityTypeBuilder<Assessment> builder) {
        builder.ToTable("assessments");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.AssessmentIdConverter());

        builder.Property(a => a.YearLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.PlanningNotes)
            .HasMaxLength(500);

        builder.Property(a => a.AssessmentType)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.HasOne<CurriculumSubject>()
            .WithMany()
            .HasForeignKey(a => a.SubjectId)
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany(s => s.Assessments)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<LessonPlan>()
            .WithMany(lp => lp.Assessments)
            .HasForeignKey(a => a.LessonPlanId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Teacher>()
            .WithMany(t => t.Assessments)
            .HasForeignKey(a => a.TeacherId)
            .IsRequired();

        builder.Property(a => a.YearLevel)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.OwnsOne(a => a.AssessmentResult, arb => {
            arb.ToTable("assessment_results");
            arb.WithOwner().HasForeignKey("AssessmentId");
            arb.Property<Guid>("Id");
            arb.HasKey("Id", "AssessmentId");

            arb.Property(ar => ar.Comments)
                .HasMaxLength(1000);

            arb.OwnsOne(ar => ar.Grade, gb => {
                gb.Property(g => g.Percentage)
                    .HasColumnName("Percentage")
                    .IsRequired();

                gb.Property(g => g.Grade)
                    .HasConversion<string>()
                    .HasMaxLength(10);
            });
        });
    }
}