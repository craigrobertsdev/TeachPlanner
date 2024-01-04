using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Database.Converters;

namespace TeachPlanner.Shared.Database.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan> {
    public void Configure(EntityTypeBuilder<LessonPlan> builder) {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.LessonPlanIdConverter());

        builder.HasOne<YearData>()
            .WithMany(yd => yd.LessonPlans)
            .HasForeignKey(lp => lp.YearDataId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<CurriculumSubject>()
            .WithMany()
            .HasForeignKey(lp => lp.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(lp => lp.Assessments)
            .WithOne()
            .HasForeignKey(a => a.LessonPlanId);

        builder.OwnsMany(lp => lp.Comments, lcb => {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonPlanId");

            lcb.Property<Guid>("Id");

            lcb.HasKey("Id", "LessonPlanId");
        });
    }
}

public class LessonPlanResourceConfiguration : IEntityTypeConfiguration<LessonPlanResource> {
    public void Configure(EntityTypeBuilder<LessonPlanResource> builder) {
        builder.ToTable("lesson_plan_resources");

        builder.HasKey(lr => new { lr.LessonPlanId, lr.ResourceId });

        builder.HasOne<LessonPlan>()
            .WithMany(lp => lp.LessonPlanResources)
            .HasForeignKey(lr => lr.LessonPlanId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Resource>()
            .WithMany(r => r.LessonPlanResources)
            .HasForeignKey(lr => lr.ResourceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}