using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.LessonPlanIdConverter());

        builder.HasOne<YearData>()
            .WithMany(yd => yd.LessonPlans)
            .HasForeignKey(lp => lp.YearDataId)
            .IsRequired();

        builder.HasOne<CurriculumSubject>()
            .WithMany()
            .HasForeignKey(lp => lp.SubjectId)
            .IsRequired();

        builder.HasMany(lp => lp.Assessments)
            .WithOne()
            .HasForeignKey(a => a.LessonPlanId);

        builder.OwnsMany(lp => lp.Comments, lcb =>
        {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonPlanId");

            lcb.Property<Guid>("Id");

            lcb.HasKey("Id", "LessonPlanId");
        });

        builder.HasOne<WeekPlanner>()
            .WithMany(wp => wp.LessonPlans)
            .HasForeignKey("WeekPlannerId");
    }
}

public class LessonPlanResourceConfiguration : IEntityTypeConfiguration<LessonPlanResource>
{
    public void Configure(EntityTypeBuilder<LessonPlanResource> builder)
    {
        builder.ToTable("lesson_plan_resources");

        builder.HasKey(lr => new { lr.LessonPlanId, lr.ResourceId });

        builder.HasOne<LessonPlan>()
            .WithMany(lp => lp.LessonPlanResources)
            .HasForeignKey(lr => lr.LessonPlanId)
            .IsRequired();

        builder.HasOne<Resource>()
            .WithMany(r => r.LessonPlanResources)
            .HasForeignKey(lr => lr.ResourceId)
            .IsRequired();
    }
}
