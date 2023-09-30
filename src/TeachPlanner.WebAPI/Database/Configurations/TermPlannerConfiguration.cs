using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TeachPlanner.Api.Entities.Common.Enums;
using TeachPlanner.Api.Entities.TermPlanners;

namespace TeachPlanner.Api.Database.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.ToTable("term_planner");

        builder.HasKey(tp => tp.Id);

        builder.Property(tp => tp.Id)
            .HasColumnName("Id");

        builder.Property(tp => tp.CalendarYear)
            .IsRequired();

#pragma warning disable CS8600, CS8603, CS8604 // Converting null literal or possible null value to non-nullable type.
        builder.Property<List<YearLevelValue>>("_yearLevels")
            .HasColumnName("YearLevels")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<YearLevelValue>>(v, (JsonSerializerOptions)null),
                new ValueComparer<List<YearLevelValue>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}

public class TermPlanConfiguration : IEntityTypeConfiguration<TermPlan>
{
    public void Configure(EntityTypeBuilder<TermPlan> builder)
    {
        builder.ToTable("term_plans");

        builder.Property("Id");
        builder.HasKey("Id");


        builder.HasMany(tp => tp.Subjects)
            .WithOne()
            .HasForeignKey("TermPlanId");

        builder.HasOne(tp => tp.TermPlanner)
            .WithMany(tp => tp.TermPlans)
            .HasForeignKey("TermPlannerId");
    }
}

