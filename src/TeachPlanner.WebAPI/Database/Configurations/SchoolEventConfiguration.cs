using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Domain.Common.Planner;

namespace TeachPlanner.Api.Database.Configurations;

public class SchoolEventConfiguration : IEntityTypeConfiguration<SchoolEvent>
{
    public void Configure(EntityTypeBuilder<SchoolEvent> builder)
    {
        builder.ToTable("school_events");

        builder.HasKey(se => se.Id);

        builder.Property(se => se.Id)
            .HasColumnName("Id");

        builder.Property(se => se.Name)
            .HasMaxLength(100);

        builder.OwnsOne(se => se.Location, lb =>
        {
            lb.Property(l => l.StreetNumber)
                .HasMaxLength(30);

            lb.Property(l => l.StreetName)
                .HasMaxLength(50);

            lb.Property(l => l.Suburb)
                .HasMaxLength(50);
        });
    }
}
