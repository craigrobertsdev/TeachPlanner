using Domain.Common.Curriculum.ValueObjects;
using Domain.Resource;
using Domain.Resource.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new ResourceId(id));

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r => r.SubjectId)
            .HasConversion(id => id.Value, id => new SubjectId(id));

        builder.Property(builder => builder.StrandId)
            .HasConversion(id => id!.Value, id => new StrandId(id));

    }
}
