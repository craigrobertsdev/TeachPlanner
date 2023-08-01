using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasConversion(lp => lp.Value, value => new LessonPlanId(value))
            .ValueGeneratedNever();

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(lp => lp.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(lp => lp.SubjectId)
            .IsRequired();

        builder.OwnsMany(lp => lp.Comments, lcb =>
        {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonPlanId");

            lcb.HasKey("Id", "LessonPlanId");
        });

        builder.OwnsMany(lp => lp.SummativeAssessmentIds, sib =>
        {
            sib.WithOwner().HasForeignKey("LessonPlanId");

            sib.ToTable("lesson_summative_assessment");
        });

        builder.OwnsMany(lp => lp.FormativeAssessmentIds, fib =>
        {
            fib.WithOwner().HasForeignKey("LessonPlanId");

            fib.ToTable("lesson_formative_assessment");
        });

        builder.OwnsMany(lp => lp.ResourceIds, rib =>
        {
            rib.WithOwner().HasForeignKey("LessonPlanId");

            rib.ToTable("lesson_resource");
        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.Comments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(LessonPlan.SummativeAssessmentIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        builder.Metadata.FindNavigation(nameof(LessonPlan.FormativeAssessmentIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}
