using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Calendar;
using TeachPlanner.Domain.WeekPlanners;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner>
{
    public void Configure(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.ToTable("week_planner");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasColumnName("Id");

        builder.HasMany(w => w.LessonPlans)
            .WithOne();

        builder.HasMany(lp => lp.SchoolEvents)
            .WithMany();

        builder.HasOne<Calendar>()
            .WithMany(c => c.WeekPlanners);
    }
}
