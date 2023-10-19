using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Database.Configurations;

public class WeekPlannerTemplateConfiguration : IEntityTypeConfiguration<WeekPlannerTemplate>
{
    public void Configure(EntityTypeBuilder<WeekPlannerTemplate> builder)
    {
        builder.ToTable("week_planner_templates");
        builder.HasKey(wp => wp.Id);
        builder.Property(wp => wp.Id)
            .HasConversion(new WeekPlannerTemplateId.StronglyTypedIdEfValueConverter());

        builder.OwnsMany(wp => wp.DayPlanTemplates, dpb =>
        {
            dpb.ToTable("day_plan_templates");
            dpb.Property<Guid>("Id");
            dpb.HasKey("Id");
            dpb.WithOwner().HasForeignKey("WeekPlannerTemplateId");

            dpb.OwnsMany(dp => dp.Periods, pb =>
            {
                pb.Property(p => p.PeriodType)
                    .HasConversion(
                        v => v.ToString(),
                        v => Enum.Parse<PeriodType>(v))
                    .HasMaxLength(20);
            });
        });
    }
}