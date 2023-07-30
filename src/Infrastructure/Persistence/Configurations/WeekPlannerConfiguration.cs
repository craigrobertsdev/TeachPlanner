using Domain.LessonPlanAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Domain.TimeTableAggregate;
using Domain.TimeTableAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner>
{
    public void Configure(EntityTypeBuilder<WeekPlanner> builder)
    {
        ConfigureWeekPlannerTable(builder);
        ConfigureWeekPlannerLessonPlanIdsTable(builder);
        ConfigureWeekPlannerSchoolEventIdsTable(builder);
    }

    private void ConfigureWeekPlannerSchoolEventIdsTable(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.OwnsMany(w => w.SchoolEventIds, seb =>
        {
            seb.ToTable("week_planner_school_event_id");

            seb.WithOwner().HasForeignKey("WeekPlannerId");

            seb.HasKey("Id");

            seb.Property(se => se.Value)
                .HasColumnName("SchoolEventId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(WeekPlanner.SchoolEventIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureWeekPlannerLessonPlanIdsTable(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.OwnsMany(w => w.LessonPlanIds, lib =>
        {
            lib.ToTable("week_planner_lesson_plan_id");

            lib.WithOwner().HasForeignKey("WeekPlannerId");

            lib.HasKey("Id");

            lib.Property(lp => lp.Value)
                .HasColumnName("LessonPlanId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(WeekPlanner.LessonPlanIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureWeekPlannerTable(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.ToTable("week_planner");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => WeekPlannerId.Create(value));

        builder.Property(w => w.TeacherId)
            .HasConversion(id => id.Value, value => TeacherIdForReference.Create(value));
    }
}
