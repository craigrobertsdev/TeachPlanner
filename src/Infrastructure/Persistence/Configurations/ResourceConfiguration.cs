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
            .HasConversion(id => id.Value, id => ResourceId.Create(id));

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r => r.SubjectId)
            .HasConversion(id => id.Value, id => SubjectId.Create(id));

        builder.Property(r => r.StrandId)
            .HasConversion(id => id!.Value, id => StrandId.Create(id));

        /*        builder.OwnsMany(r => r.LessonIds, lpb =>
                {
                    lpb.WithOwner().HasForeignKey("ResourceId");
                })
                    .Metadata.FindNavigation(nameof(Resource.LessonIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        */




    }
}
