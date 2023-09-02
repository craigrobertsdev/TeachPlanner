using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanner;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;
public class YearPlannerCorfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.ToTable("year_planner");

        builder.HasKey(yp => yp.Id);

        builder.Property(yp => yp.Id)
            .HasColumnName("Id");

        builder.Property(yp => yp.YearLevel)
            .HasConversion<string>();

        builder.OwnsMany(yp => yp.TermPlans, tpb =>
        {
            tpb.ToTable("term_plans");

            tpb.WithOwner().HasForeignKey("YearPlannerId");

            tpb.HasKey("Id", "YearPlannerId");

#pragma warning disable CS8603 // Possible null reference return.
            tpb.Property(tp => tp.Subjects)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<Guid, List<Strand>>>(v, new JsonSerializerOptions()),
                    null);
        });
    }
}

