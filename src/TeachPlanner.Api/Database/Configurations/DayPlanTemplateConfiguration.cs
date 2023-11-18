using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Database.Configurations;

public class DayPlanTemplateConfiguration : IEntityTypeConfiguration<DayPlanTemplate>
{
    public void Configure(EntityTypeBuilder<DayPlanTemplate> builder)
    {
        builder.ToTable("day_plan_templates");
        builder.HasKey(dp => dp.Id);
        builder.Property(dp => dp.Id)
            .HasConversion(new DayPlanTemplateId.StronglyTypedIdEfValueConverter());

        builder.OwnsMany(dp => dp.Periods, pb =>
        {
            pb.ToTable("template_periods");
            pb.Property<Guid>("Id");
            pb.HasKey("Id");
            pb.Property(p => p.PeriodType)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<PeriodType>(v))
                .HasMaxLength(20);
        });
    }
}