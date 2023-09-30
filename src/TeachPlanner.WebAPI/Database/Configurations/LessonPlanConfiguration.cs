using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.WeekPlanners;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Database.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasColumnName("Id");

        builder.HasOne<YearData>()
            .WithMany(t => t.LessonPlans)
            .HasForeignKey(lp => lp.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .IsRequired();

        builder.HasMany(lp => lp.Assessments)
            .WithOne();

        builder.OwnsMany(lp => lp.Comments, lcb =>
        {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonPlanId");

            lcb.Property<Guid>("Id");

            lcb.HasKey("Id", "LessonPlanId");
        });

        builder.HasMany(lp => lp.Assessments)
            .WithOne();

        builder.HasOne<WeekPlanner>()
            .WithMany(wp => wp.LessonPlans)
            .HasForeignKey("WeekPlannerId");
    }
}
