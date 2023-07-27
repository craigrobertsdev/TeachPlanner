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
            wib.ToTable("TermPlannerWeekPlannerIds");

            wib.WithOwner().HasForeignKey("TermPlannerId");

            wib.HasKey("Id");

            wib.Property(wib => wib.Value)
                .ValueGeneratedNever()
                .HasColumnName("WeekPlannerI");
        });

        builder.Metadata.FindNavigation(nameof(TermPlanner.WeekPlannerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTermPlannerSchoolEventIdsTable(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.OwnsMany(t => t.SchoolEventIds, seb =>
        {
            seb.ToTable("TermPlannerSchoolEventIds");

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
        builder.ToTable("TermPlanner");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => new TermPlannerId(value));
    }
}
