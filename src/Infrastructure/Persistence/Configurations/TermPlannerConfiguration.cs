using Domain.TermPlannerAggregate;
using Domain.TermPlannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
    {
        ConfigureTermPlannerTable(builder);
        ConfigureTermPlannerSchoolEventIdsTable(builder);
        ConfigureTermPlannerWeekPlannerIdsTable(builder);
    }

    private void ConfigureTermPlannerWeekPlannerIdsTable(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.OwnsMany(t => t.WeekPlannerIds, wib =>
        {
            wib.ToTable("term_planner_week_planner_id");

            wib.WithOwner().HasForeignKey("TermPlannerId");

            wib.HasKey("Id");

            wib.Property(wib => wib.Value)
                .ValueGeneratedNever()
                .HasColumnName("WeekPlannerId");
        });

        builder.Metadata.FindNavigation(nameof(TermPlanner.WeekPlannerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTermPlannerSchoolEventIdsTable(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.OwnsMany(t => t.SchoolEventIds, seb =>
        {
            seb.ToTable("term_planner_school_event_id");

            seb.WithOwner().HasForeignKey("TermPlannerId");

            seb.HasKey("Id");

            seb.Property(seb => seb.Value)
                .ValueGeneratedNever()
                .HasColumnName("SchoolEventId");
        });

        builder.Metadata.FindNavigation(nameof(TermPlanner.SchoolEventIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTermPlannerTable(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.ToTable("term_planner");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TermPlannerId.Create(value));
    }
}
