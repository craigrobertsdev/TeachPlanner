using Domain.Common.Curriculum.ValueObjects;
using Domain.LessonPlanAggregate;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        ConfigureLessonTable(builder);
        ConfigureLessonResourceIdsTable(builder);
        ConfigureLessonAssessmentIdsTable(builder);
    }

    private void ConfigureLessonAssessmentIdsTable(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.OwnsMany(l => l.AssessmentIds, aib =>
        {
            aib.ToTable("LessonAssessmentIds");

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
            rib.ToTable("LessonResourceIds");

            rib.WithOwner().HasForeignKey("LessonId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("ResourceId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.ResourceIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    private void ConfigureLessonTable(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("LessonPlans");

        builder.HasKey(lp => lp.Id);
        builder.Property(lp => lp.Id)
            .HasConversion(lp => lp.Value, value => new LessonPlanId(value))
            .ValueGeneratedNever();

        builder.Property(lp => lp.TeacherId)
            .HasConversion(t => t.Value, value => new TeacherId(value));

        builder.Property(lp => lp.SubjectId)
            .HasConversion(id => id.Value, value => new SubjectId(value));

        builder.OwnsMany(lp => lp.Comments, cb =>
        {
            cb.ToTable("Comments");

            cb.WithOwner().HasForeignKey("LessonId");

            cb.HasKey(lp => lp.Id);

            cb.Property(lp => lp.Id)
                .HasColumnName("CommentId")
                .HasConversion(id => id.Value, value => new CommentId(value))
                .ValueGeneratedNever();

        });

        builder.Metadata.FindNavigation(nameof(LessonPlan.Comments))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
