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

        builder.OwnsMany(wp => wp.DayPlans, dpb =>
        {
            dpb.ToTable("day_plan_templates");
            dpb.Property<Guid>("Id");
            dpb.HasKey("Id");

            dpb.Property(dp => dp.Periods)
                .HasConversion(
                    v => string.Join(',', v.Select(x => (int)x)),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Enum.Parse<PeriodType>(x))
                        .ToList())
                .HasMaxLength(20);
        });
    }
}