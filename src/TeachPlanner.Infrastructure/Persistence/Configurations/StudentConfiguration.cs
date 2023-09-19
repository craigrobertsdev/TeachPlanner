using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Students;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable("students");

        builder.Property(s => s.Id)
            .HasColumnName("Id");

        builder.HasMany(s => s.Reports)
            .WithOne()
            .IsRequired();

        builder.HasMany(s => s.Assessments)
            .WithOne()
            .IsRequired();
    }
}

public class StudentsForYearConfiguration : IEntityTypeConfiguration<StudentsForYear>
{
    public void Configure(EntityTypeBuilder<StudentsForYear> builder)
    {
        builder.ToTable("students_for_year");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany(sfy => sfy.Students)
            .WithMany();

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
