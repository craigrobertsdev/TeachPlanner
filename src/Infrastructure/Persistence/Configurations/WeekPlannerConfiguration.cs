using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Teacher;
using TeachPlanner.Domain.WeekPlanner;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner>
{
    public void Configure(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.ToTable("week_planner");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasColumnName("Id");

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(w => w.TeacherId)
            .IsRequired();

        builder.HasMany<LessonPlan>()
            .WithOne()
            .HasForeignKey("WeekPlannerId");

        builder.HasMany(lp => lp.SchoolEvents)
            .WithMany();

        /*        builder.Navigation(wp => wp.LessonPlanIds).Metadata.SetField("_lessonPlanIds");
                builder.Navigation(wp => wp.LessonPlanIds).UsePropertyAccessMode(PropertyAccessMode.Field);
        */
    }
}
