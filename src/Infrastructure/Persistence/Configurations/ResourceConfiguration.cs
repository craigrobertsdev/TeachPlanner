using Domain.LessonPlanAggregate;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("resources");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new ResourceId(id));

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .IsRequired();

    }
}
