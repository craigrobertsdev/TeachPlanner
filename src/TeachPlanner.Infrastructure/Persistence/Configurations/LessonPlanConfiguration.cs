using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.WeekPlanners;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

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
