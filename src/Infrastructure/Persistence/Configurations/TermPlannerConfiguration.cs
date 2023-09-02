using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Calendar;
using TeachPlanner.Domain.WeekPlanner;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable("term_planner");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id");

        builder.HasMany<WeekPlanner>()
            .WithOne()
            .IsRequired();

        builder.HasMany(tp => tp.SchoolEvents)
            .WithMany();
    }
}
