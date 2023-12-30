using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Database.Converters;
using TeachPlanner.Shared.Domain.Calendar;

namespace TeachPlanner.Shared.Database.Configurations;

public class CalendarConfiguration : IEntityTypeConfiguration<Calendar> {
    public void Configure(EntityTypeBuilder<Calendar> builder) {
        builder.ToTable("calendar");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.CalendarIdConverter());

        builder.HasMany(tp => tp.SchoolEvents)
            .WithMany();
    }
}