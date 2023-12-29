using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Blazor.Database.Converters;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Database.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource> {
    public void Configure(EntityTypeBuilder<Resource> builder) {
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

        builder.HasOne<CurriculumSubject>()
            .WithMany()
            .HasForeignKey(r => r.SubjectId)
            .IsRequired();
    }
}