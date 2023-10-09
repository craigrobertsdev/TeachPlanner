using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Database.Configurations;

public class CurriculumSubjectConfiguration : IEntityTypeConfiguration<CurriculumSubject>
{
    public void Configure(EntityTypeBuilder<CurriculumSubject> builder)
    {
        builder.ToTable("curriculum_subjects");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.CurriculumSubjectIdConverter());

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

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        builder.HasMany(s => s.ContentDescriptions)
            .WithOne()
            .HasForeignKey("StrandId")
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

        builder.HasMany(cd => cd.Elaborations)
            .WithOne()
            .HasForeignKey("ContentDescriptionId")
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

        builder.Property(e => e.Description)
            .HasMaxLength(1000);
    }
}
