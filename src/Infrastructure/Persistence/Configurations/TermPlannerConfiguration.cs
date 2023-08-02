using Domain.Common.Planner;
using Domain.TermPlannerAggregate;
using Domain.TimeTableAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.ToTable("term_planner");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => new TermPlannerId(value));

        builder.HasMany<WeekPlanner>()
            .WithOne()
            .IsRequired();

        builder.HasMany(tp => tp.SchoolEvents)
            .WithMany();

        builder.Navigation(tp => tp.WeekPlannerIds).Metadata.SetField("_weekPlannerIds");
        builder.Navigation(tp => tp.WeekPlannerIds).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(tp => tp.SchoolEvents).Metadata.SetField("_schoolEvents");
        builder.Navigation(tp => tp.SchoolEvents).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
