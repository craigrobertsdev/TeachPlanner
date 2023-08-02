using Domain.SubjectAggregates;
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
            .HasColumnName("Id");

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        builder.HasMany(s => s.YearLevels)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}

public class YearLevelConfiguration : IEntityTypeConfiguration<YearLevel>
{
    public void Configure(EntityTypeBuilder<YearLevel> builder)
    {
        builder.ToTable("year_levels");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany(yl => yl.Strands)
            .WithOne()
            .IsRequired()
            .HasForeignKey("YearLevelId")
            .OnDelete(DeleteBehavior.Cascade);

    }
}

public class StrandConfiguration : IEntityTypeConfiguration<Strand>
{
    public void Configure(EntityTypeBuilder<Strand> builder)
    {
        builder.ToTable("strands");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany<Substrand>()
            .WithOne()
            .HasForeignKey("StrandId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<ContentDescriptor>()
            .WithOne()
            .HasForeignKey("StrandId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SubstrandConfiguration : IEntityTypeConfiguration<Substrand>
{
    public void Configure(EntityTypeBuilder<Substrand> builder)
    {
        builder.ToTable("substrands");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany<ContentDescriptor>()
            .WithOne()
            .HasForeignKey("SubstrandId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}

public class ContentDescriptorConfiguration : IEntityTypeConfiguration<ContentDescriptor>
{
    public void Configure(EntityTypeBuilder<ContentDescriptor> builder)
    {
        builder.ToTable("content_descriptors");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.HasMany<Elaboration>()
            .WithOne()
            .HasForeignKey("ContentDescriptorId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ElaborationConfiguration : IEntityTypeConfiguration<Elaboration>
{
    public void Configure(EntityTypeBuilder<Elaboration> builder)
    {
        builder.ToTable("elaborations");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");
    }
}
