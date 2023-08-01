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

        builder.HasMany<SchoolEvent>()
            .WithMany();
    }
}
