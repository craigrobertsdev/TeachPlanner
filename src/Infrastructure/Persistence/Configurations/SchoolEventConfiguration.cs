using Domain.Common.Planner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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

        builder.OwnsOne(se => se.Location);
    }
}
