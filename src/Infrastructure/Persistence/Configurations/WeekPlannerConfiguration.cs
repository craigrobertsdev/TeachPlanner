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
        //ConfigureWeekPlannerLessonPlanIdsTable(builder);
        //ConfigureWeekPlannerSchoolEventIdsTable(builder);
    }

    //private void ConfigureWeekPlannerSchoolEventIdsTable(EntityTypeBuilder<WeekPlanner> builder)
    //{
    //    builder.OwnsMany(w => w.SchoolEventIds, seb =>
    //    {
    //        seb.ToTable("WeekPlannerSchoolEventIds");

    //        seb.WithOwner().HasForeignKey("WeekPlannerId");

    //        seb.HasKey("Id");

    //        seb.Property(se => se.Value)
    //            .HasColumnName("SchoolEventId")
    //            .ValueGeneratedNever();
    //    });

    //    builder.Metadata.FindNavigation(nameof(WeekPlanner.SchoolEventIds))!
    //        .SetPropertyAccessMode(PropertyAccessMode.Field);
    //}

    private void ConfigureWeekPlannerLessonPlanIdsTable(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.OwnsMany(w => w.LessonPlanIds, lib =>
        {
            lib.ToTable("WeekPlannerLessonPlanIds");

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
        builder.ToTable("WeekPlanners");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new WeekPlannerId(value));

        builder.Property(w => w.TeacherId)
            .HasConversion(id => id.Value, value => new TeacherId(value));
    }
}
