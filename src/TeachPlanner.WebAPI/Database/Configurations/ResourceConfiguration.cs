using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("resources");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.ResourceIdConverter());

        builder.Property(r => r.Name)
            .HasMaxLength(100);

        builder.Property(r => r.Url)
            .HasMaxLength(300);

        builder.Property(r => r.AssociatedStrands)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                null);

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .IsRequired();
    }
}
