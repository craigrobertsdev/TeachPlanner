using Domain.LessonPlanAggregate.ValueObjects;
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
        builder.ToTable("Resources");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new ResourceId(id));

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r => r.SubjectId)
            .HasConversion(id => id.Value, id => new SubjectId(id));

        builder.Property(r => r.StrandId)
            .HasConversion(id => id!.Value, id => new StrandId(id));

        /*        builder.OwnsMany(r => r.LessonIds, lpb =>
                {
                    lpb.WithOwner().HasForeignKey("ResourceId");
                })
                    .Metadata.FindNavigation(nameof(Resource.LessonIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        */

        builder.Property



    }
}
