using Domain.SubjectAggregates;
using Domain.YearPlannerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Persistence.Configurations;
public class YearPlannerCorfiguration : IEntityTypeConfiguration<YearPlanner>
{
    public void Configure(EntityTypeBuilder<YearPlanner> builder)
    {
        builder.ToTable("year_planner");

        builder.HasKey(yp => yp.Id);

        builder.Property(yp => yp.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new YearPlannerId(value));

        builder.Property(yp => yp.YearLevel)
            .HasConversion<string>();

        builder.ToTable("term_plan");

        builder.OwnsMany(yp => yp.TermPlans, tpb =>
        {
            tpb.WithOwner().HasForeignKey("YearPlannerId");

            tpb.HasKey("Id", "YearPlannerId");

#pragma warning disable CS8603 // Possible null reference return.
            tpb.Property(tp => tp.Subjects)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<SubjectId, List<Strand>>>(v, new JsonSerializerOptions())
                );
        });
#pragma warning restore CS8603 // Possible null reference return.

        builder.Metadata.FindNavigation(nameof(YearPlanner.TermPlans))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

