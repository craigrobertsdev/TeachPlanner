using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Database.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner>
{
    public void Configure(EntityTypeBuilder<WeekPlanner> builder)
    {
        builder.ToTable("week_planner");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.WeekPlannerIdConverter());

        builder.HasMany(wp => wp.DayPlans)
            .WithOne()
            .HasForeignKey("WeekPlannerId");

        builder.HasOne(wp => wp.DayPlanTemplate)
            .WithMany();
    }
}