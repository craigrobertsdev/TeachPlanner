using Domain.SubjectAggregates.ValueObjects;
using Domain.YearPlannerAggregate;
using Domain.YearPlannerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Persistence.Configurations;
public class YearPlannerCorfiguration : IEntityTypeConfiguration<YearPlanner>
{
    public void Configure(EntityTypeBuilder<YearPlanner> builder)
    {
        ConfigureYearPlannerTable(builder);
        ConfigureTermPlansTable(builder);
    }

    private void ConfigureYearPlannerTable(EntityTypeBuilder<YearPlanner> builder)
    {
        builder.ToTable("YearPlanner");

        builder.HasKey(yp => yp.Id);

        builder.Property(yp => yp.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new YearPlannerId(value));

        builder.Property(yp => yp.YearLevel)
            .HasConversion<string>();
    }

    private void ConfigureTermPlansTable(EntityTypeBuilder<YearPlanner> builder)
    {
        builder.OwnsMany(yp => yp.TermPlans, tpb =>
         {
             tpb.ToTable("TermPlans");

             tpb.WithOwner().HasForeignKey("YearPlannerId");

             tpb.HasKey(tp => tp.Id);

             tpb.Property(tp => tp.Id)
                 .ValueGeneratedNever()
                 .HasConversion(id => id.Value, value => new TermPlanId(value));

#pragma warning disable CS8603 // Possible null reference return.
             tpb.Property(tp => tp.Subjects)
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                     v => JsonSerializer.Deserialize<Dictionary<SubjectId, List<StrandId>>>(v, new JsonSerializerOptions())
                 );
         });
#pragma warning restore CS8603 // Possible null reference return.


        builder.Metadata.FindNavigation(nameof(YearPlanner.TermPlans))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
