using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Api.Database.Converters;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("subjects");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("Id")
            .HasConversion(new StronglyTypedIdConverter.SubjectIdConverter());

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        builder.HasMany(s => s.YearLevels)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        //    builder.OwnsMany(s => s.YearLevels, ylb =>
        //    {
        //        ylb.ToTable("year_levels");
        //        ylb.WithOwner().HasForeignKey("SubjectId");

        //        ylb.Property<Guid>("Id");
        //        ylb.HasKey("Id");

        //        ylb.Ignore(yl => yl.Name);

        //        ylb.Property(yl => yl.YearLevelValue)
        //            .HasConversion(
        //                v => (int?)v,
        //                v => (YearLevelValue?)v);

        //        ylb.Property(yl => yl.BandLevelValue)
        //            .HasConversion(
        //                v => (int?)v,
        //                v => (BandLevelValue?)v);

        //        ylb.OwnsMany(yl => yl.Strands, sb =>
        //        {
        //            sb.ToTable("strands");
        //            sb.WithOwner().HasForeignKey("YearLevelId");

        //            sb.Property<Guid>("Id");
        //            sb.HasKey("Id");

        //            sb.Property(s => s.Name)
        //                .HasMaxLength(50);

        //            sb.OwnsMany(s => s.ContentDescriptions, cdb =>
        //            {
        //                cdb.ToTable("content_descriptions");
        //                cdb.WithOwner().HasForeignKey("StrandId");

        //                cdb.Property<Guid>("Id");
        //                cdb.HasKey("Id");

        //                cdb.Property(cd => cd.Description)
        //                    .HasMaxLength(1000);

        //                cdb.Property(cd => cd.CurriculumCode)
        //                .HasMaxLength(50);

        //                cdb.OwnsMany(cd => cd.Elaborations, elb =>
        //                {
        //                    elb.ToTable("elaborations");
        //                    elb.WithOwner().HasForeignKey("ContentDescriptionId");

        //                    elb.Property<Guid>("Id");
        //                    elb.HasKey("Id");

        //                    elb.Property(e => e.Description)
        //                        .HasMaxLength(1000);
        //                });
        //            });
        //        });
        //    });
        //}
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
