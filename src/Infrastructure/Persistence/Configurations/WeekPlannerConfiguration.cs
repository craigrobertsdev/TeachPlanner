using Domain.Common.Planner;
using Domain.LessonPlanAggregate;
using Domain.TeacherAggregate;
using Domain.TimeTableAggregate;
using Domain.WeekPlannerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner>
{
    public void Configure(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.ToTable("week_planner");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new WeekPlannerId(value));

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(w => w.TeacherId)
            .IsRequired();

        builder.HasMany<LessonPlan>()
            .WithOne()
            .HasForeignKey("WeekPlannerId");

        builder.HasMany(lp => lp.SchoolEvents)
            .WithMany();

        builder.Navigation(wp => wp.LessonPlanIds).Metadata.SetField("_lessonPlanIds");
        builder.Navigation(wp => wp.LessonPlanIds).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
