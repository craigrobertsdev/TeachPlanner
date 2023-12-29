using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Blazor.Database.Converters;
using TeachPlanner.Shared.Domain.Calendar;

namespace TeachPlanner.Blazor.Database.Configurations;

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