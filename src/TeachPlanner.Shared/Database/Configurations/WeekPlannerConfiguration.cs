using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.WeekPlanners;

namespace TeachPlanner.Shared.Database.Configurations;

public class WeekPlannerConfiguration : IEntityTypeConfiguration<WeekPlanner> {
    public void Configure(EntityTypeBuilder<WeekPlanner> builder) {
        builder.ToTable("week_planner");
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.WeekPlannerIdConverter());

        builder.HasMany(wp => wp.DayPlans)
            .WithOne()
            .HasForeignKey("WeekPlannerId");

        builder.HasOne<DayPlanTemplate>()
            .WithMany()
            .HasForeignKey(wp => wp.DayPlanTemplateId);
    }
}