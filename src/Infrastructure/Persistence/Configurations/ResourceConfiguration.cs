using Domain.ResourceAggregate;
using Domain.ResourceAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
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
            .HasConversion(id => id.Value, id => ResourceId.Create(id));

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r => r.SubjectId)
            .HasConversion(id => id.Value, id => SubjectIdForReference.Create(id));

        builder.Property(r => r.StrandId)
            .HasConversion(id => id!.Value, id => StrandIdForReference.Create(id));
    }
}
