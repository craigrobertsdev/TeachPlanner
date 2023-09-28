using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.YearDataRecords;

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

        builder.HasMany(t => t.SummativeAssessments)
            .WithOne()
            .IsRequired();

        builder.HasMany(t => t.FormativeAssessments)
            .WithOne()
            .IsRequired();

#pragma warning disable CS8600, CS8603, CS8604 // Converting null literal or possible null value to non-nullable type.
        builder.Property<Dictionary<int, Guid>>("_yearDataHistory")
            .HasColumnName("YearDataHistory")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Dictionary<int, Guid>>(v, (JsonSerializerOptions)null),
                new ValueComparer<Dictionary<int, Guid>>(
                    (d1, d2) => (d1.SequenceEqual(d2)),
                    d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    d => d.ToDictionary(x => x.Key, x => x.Value)));
    }
}

