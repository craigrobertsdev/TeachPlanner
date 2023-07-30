using Domain.LessonPlanAggregate;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        ConfigureLessonPlanTable(builder);
        ConfigureLessonResourceIdsTable(builder);
        ConfigureLessonAssessmentIdsTable(builder);
    }

    private void ConfigureLessonAssessmentIdsTable(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.OwnsMany(l => l.AssessmentIds, aib =>
        {
            aib.ToTable("lesson_assessment_ids");

            aib.WithOwner().HasForeignKey("LessonId");

            aib.HasKey("Id");

            aib.Property(a => a.Value)
                .HasColumnName("AssessmentId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.AssessmentIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureLessonResourceIdsTable(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.OwnsMany(l => l.ResourceIds, rib =>
        {
            rib.ToTable("lesson_resource_ids");

            rib.WithOwner().HasForeignKey("LessonId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("ResourceId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.ResourceIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureLessonPlanTable(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);
        builder.Property(lp => lp.Id)
            .HasConversion(lp => lp.Value, value => LessonPlanId.Create(value))
            .ValueGeneratedNever();

        builder.Property(lp => lp.TeacherId)
            .HasConversion(t => t.Value, value => TeacherIdForReference.Create(value));

        builder.Property(lp => lp.SubjectId)
            .HasConversion(id => id.Value, value => SubjectIdForReference.Create(value));

        builder.OwnsMany(lp => lp.Comments, lcb =>
        {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonId");

            lcb.HasKey(lc => lc.Id);

            lcb.Property(lc => lc.Id)
                .HasColumnName("LessonCommentId")
                .HasConversion(id => id.Value, value => LessonCommentId.Create(value))
                .ValueGeneratedNever();

        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.Comments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
