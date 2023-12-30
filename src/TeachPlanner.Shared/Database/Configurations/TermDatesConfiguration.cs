using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Shared.Domain.PlannerTemplates;

namespace TeachPlanner.Shared.Database.Configurations;

public class TermDatesConfiguration : IEntityTypeConfiguration<TermDate> {
    public void Configure(EntityTypeBuilder<TermDate> builder) {
        builder.ToTable("term_dates");
        builder.Property<Guid>("Id");
        builder.HasKey("Id");
    }
}
