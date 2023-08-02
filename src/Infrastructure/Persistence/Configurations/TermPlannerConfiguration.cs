using Domain.Common.Planner;
using Domain.TermPlannerAggregate;
using Domain.WeekPlannerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class TermPlannerConfiguration : IEntityTypeConfiguration<TermPlanner>
{
    public void Configure(EntityTypeBuilder<TermPlanner> builder)
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
