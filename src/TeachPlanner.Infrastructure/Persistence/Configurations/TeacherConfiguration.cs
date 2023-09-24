using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System.Text.Json;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id");

        builder.Property(t => t.FirstName).HasMaxLength(50);

        builder.Property(t => t.LastName).HasMaxLength(50);

        builder.HasMany(t => t.YearDataHistory)
            .WithOne();

        builder.HasMany(t => t.SummativeAssessments)
            .WithOne()
            .IsRequired();

        builder.HasMany(t => t.FormativeAssessments)
            .WithOne()
            .IsRequired();

        builder.HasMany(t => t.Reports)
            .WithOne()
            .HasForeignKey(r => r.TeacherId)
            .IsRequired();

        builder.HasMany(t => t.LessonPlans)
           .WithOne()
           .HasForeignKey(lp => lp.TeacherId);

        builder.HasMany(t => t.TermPlanners)
            .WithOne()
            .HasForeignKey(tp => tp.TeacherId);

        builder.HasMany(t => t.YearDataHistory)
            .WithOne()
            .HasForeignKey("TeacherId");

    }
}

public class YearDataConfiguration : IEntityTypeConfiguration<YearData>
{
    public void Configure(EntityTypeBuilder<YearData> builder)
    {
        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany(yd => yd.Students)
            .WithOne();

        builder.HasMany(yd => yd.Subjects)
            .WithMany();

        builder.HasOne<Teacher>()
            .WithMany(t => t.YearDataHistory)
            .HasForeignKey("TeacherId");

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
