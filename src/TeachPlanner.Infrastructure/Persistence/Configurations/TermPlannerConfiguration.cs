using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Helpers;
using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
    {
        builder.ToTable("term_planner");

        builder.HasKey(yp => yp.Id);

        builder.Property(yp => yp.Id)
            .HasColumnName("Id");

        builder.Property(yp => yp.CalendarYear)
            .IsRequired();

        builder.Property(yp => yp.YearLevel)
            .HasConversion<string>()
            .HasMaxLength(15);

#pragma warning disable CS8600, CS8603, CS8604 // Converting null literal or possible null value to non-nullable type.
        builder.Property<List<YearLevelValue>>("_yearLevels")
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

        builder.HasKey(tp => tp.Id);

        builder.Property<List<string>>("_curriculumCodes")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null),
                new ValueComparer<List<string>>(
                    (c1, c2) => HelperMethods.ListsContainSameElements(c1, c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}

