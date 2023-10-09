using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Calendar;

namespace TeachPlanner.Api.Database.Configurations;
public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable("calendar");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.CalendarIdConverter());

        builder.HasMany(c => c.WeekPlanners)
            .WithOne()
            .IsRequired();

        builder.HasMany(tp => tp.SchoolEvents)
            .WithMany();
    }
}
