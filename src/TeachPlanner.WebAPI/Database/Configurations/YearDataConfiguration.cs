using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System.Text.Json;
using TeachPlanner.Api.Entities.Common.Enums;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.TermPlanners;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Database.Configurations;
public class YearDataConfiguration : IEntityTypeConfiguration<YearData>
{
    public void Configure(EntityTypeBuilder<YearData> builder)
    {
        builder.HasKey(yd => yd.Id);

        builder.HasMany(yd => yd.Students)
            .WithOne();

        builder.HasMany(yd => yd.Subjects)
            .WithMany();

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(yd => yd.TeacherId);

        builder.HasOne(yd => yd.TermPlanner)
            .WithOne()
            .HasForeignKey<TermPlanner>(tp => tp.YearDataId);

        builder.HasMany(yd => yd.Reports)
            .WithOne()
            .HasForeignKey(r => r.YearDataId);

        builder.HasMany(yd => yd.LessonPlans)
            .WithOne()
            .HasForeignKey(lp => lp.YearDataId);

        builder.HasMany(yd => yd.WeekPlanners)
            .WithOne()
            .HasForeignKey(wp => wp.YearDataId);

#pragma warning disable CS8600, CS8603, CS8604 // Converting null literal or possible null value to non-nullable type.
        builder.Property<List<YearLevelValue>>("_yearLevelsTaught")
            .HasColumnName("YearLevels")
            .HasMaxLength(100)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<YearLevelValue>>(v, (JsonSerializerOptions)null),
                new ValueComparer<List<YearLevelValue>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));

    }
}
