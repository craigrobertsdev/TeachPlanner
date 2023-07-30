using Domain.SubjectAggregates;
using Domain.SubjectAggregates.Entities;
using Domain.SubjectAggregates.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("subjects");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("SubjectId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => SubjectId.Create(id));

        builder.HasMany(s => s.YearLevels)
            .WithOne()
            .HasForeignKey("Subject")
            .IsRequired();

        builder.Navigation(s => s.YearLevels).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class YearLevelConfiguration : IEntityTypeConfiguration<YearLevel>
{
    public void Configure(EntityTypeBuilder<YearLevel> builder)
    {
        builder.ToTable("year_levels");

        builder.HasKey(yl => yl.Id);

        builder.Property(yl => yl.Id)
            .HasColumnName("YearLevelId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => YearLevelId.Create(id));

        builder.HasMany(yl => yl.Strands)
            .WithOne()
            .HasForeignKey("YearLevel")
            .IsRequired();

        builder.Navigation(yl => yl.Strands).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class StrandConfiguration : IEntityTypeConfiguration<Strand>
{
    public void Configure(EntityTypeBuilder<Strand> builder)
    {
        builder.ToTable("strands");

        builder.HasKey(sb => sb.Id);

        builder.Property(sb => sb.Id)
            .HasColumnName("StrandId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => StrandId.Create(id));

        builder.HasMany<Substrand>("_substrands")
            .WithOne()
            .HasForeignKey("Strand")
            .IsRequired();

        builder.HasMany<ContentDescriptor>("_contentDescriptors")
            .WithOne()
            .HasForeignKey("Strand")
            .IsRequired();
    }
}

public class SubstrandConfiguration : IEntityTypeConfiguration<Substrand>
{
    public void Configure(EntityTypeBuilder<Substrand> builder)
    {
        builder.ToTable("substrands");

        builder.HasKey(sb => sb.Id);

        builder.Property(sb => sb.Id)
            .HasColumnName("SubstrandId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => SubstrandId.Create(id));

        builder.HasMany(ss => ss.ContentDescriptors)
            .WithOne()
            .HasForeignKey("Substrand")
            .IsRequired();
    }
}

public class ContentDescriptorConfiguration : IEntityTypeConfiguration<ContentDescriptor>
{
    public void Configure(EntityTypeBuilder<ContentDescriptor> builder)
    {
        builder.ToTable("content_descriptors");

        builder.HasKey(cd => cd.Id);

        builder.Property(cd => cd.Id)
            .HasColumnName("ContentDescriptorId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => ContentDescriptorId.Create(id));

        builder.HasMany(cd => cd.Elaborations)
            .WithOne()
            .HasForeignKey("ContentDescriptor")
            .IsRequired();
    }
}

public class ElaborationConfiguration : IEntityTypeConfiguration<Elaboration>
{
    public void Configure(EntityTypeBuilder<Elaboration> builder)
    {
        builder.ToTable("elaborations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Elaboration")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => ElaborationId.Create(id));
    }
}
