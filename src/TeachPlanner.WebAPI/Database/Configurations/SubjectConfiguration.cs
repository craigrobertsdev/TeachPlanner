using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Entities.Common.Enums;
using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Database.Configurations;

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

        builder.Ignore(yl => yl.Name);

        builder.Property(yl => yl.YearLevelValue)
            .HasConversion(
                v => (int?)v,
                v => (YearLevelValue?)v);

        builder.Property(yl => yl.BandLevelValue)
            .HasConversion(
                v => (int?)v,
                v => (BandLevelValue?)v);

        builder.HasOne(yl => yl.Subject)
            .WithMany(s => s.YearLevels)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(yl => yl.Strands)
            .WithOne(s => s.YearLevel)
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

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        builder.HasOne(s => s.YearLevel)
            .WithMany(yl => yl.Strands)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Substrands)
            .WithOne(ss => ss.Strand)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.ContentDescriptions)
            .WithOne(cd => cd.Strand)
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

        builder.Property(ss => ss.Name)
            .HasMaxLength(50);

        builder.HasOne(ss => ss.Strand)
            .WithMany(s => s.Substrands)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ss => ss.ContentDescriptions)
            .WithOne(cd => cd.Substrand)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ContentDescriptionConfiguration : IEntityTypeConfiguration<ContentDescription>
{
    public void Configure(EntityTypeBuilder<ContentDescription> builder)
    {
        builder.ToTable("content_descriptions");

        builder.Property<Guid>("Id");

        builder.HasKey("Id");

        builder.Property(cd => cd.Description)
            .HasMaxLength(1000);

        builder.Property(cd => cd.CurriculumCode)
            .HasMaxLength(50);

        builder.HasOne(cd => cd.Substrand)
            .WithMany(ss => ss.ContentDescriptions)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cd => cd.Strand)
            .WithMany(s => s.ContentDescriptions)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cd => cd.Elaborations)
            .WithOne(el => el.ContentDescription)
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

        builder.HasOne(el => el.ContentDescription)
            .WithMany(cd => cd.Elaborations)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);
    }
}
