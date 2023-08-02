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
            .HasColumnName("SubjectId")
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new SubjectId(id));

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        /*        builder.OwnsMany(s => s.YearLevels, ylb =>
                {
                    ylb.ToTable("year_levels");

                    ylb.WithOwner();

                    ylb.Property<int>("Id");

                    ylb.HasKey("Id");

                    ylb.Property(yl => yl.YearLevelValue)
                        .HasConversion<string>()
                        .HasMaxLength(25);

                    ylb.Property(yl => yl.Name)
                        .HasMaxLength(50);

                    ylb.OwnsMany(yl => yl.Strands, sb =>
                    {
                        sb.ToTable("strands");

                        sb.WithOwner();

                        sb.Property<int>("Id");

                        sb.HasKey("Id");

                        sb.Property(s => s.Name)
                            .HasMaxLength(50);

                        sb.OwnsMany(typeof(Substrand), "_substrands", ssb =>
                        {
                            ssb.ToTable("substrands");

                            ssb.WithOwner();

                            ssb.Property<int>("Id");

                            ssb.HasKey("Id");

                            ssb.Property("Name")
                                .HasMaxLength(50);

                            ssb.OwnsMany(typeof(ContentDescriptor), "_contentDescriptors", cdb =>
                            {
                                cdb.ToTable("content_descriptors");

                                // Substrand may not exist in certain subjects
                                cdb.WithOwner();

                                cdb.Property<int>("Id");

                                cdb.HasKey("Id");

                                cdb.OwnsMany(typeof(Elaboration), "_elaborations", eb =>
                                {
                                    eb.ToTable("elaborations");

                                    eb.WithOwner();

                                    eb.Property<int>("Id");

                                    eb.HasKey("Id");

                                });
                            });
                        });
                    });
                });
        */
        builder.HasMany(s => s.YearLevels)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(s => s.YearLevels).Metadata.SetField("_yearLevels");
        builder.Navigation(s => s.YearLevels).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class YearLevelConfiguration : IEntityTypeConfiguration<YearLevel>
{
    public void Configure(EntityTypeBuilder<YearLevel> builder)
    {
        builder.ToTable("year_levels");

        builder.Property<int>("Id");

        builder.HasKey("Id");

        builder.HasMany(yl => yl.Strands)
            .WithOne()
            .IsRequired()
            .HasForeignKey("YearLevelId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(yl => yl.Strands).Metadata.SetField("_strands");
        builder.Navigation(yl => yl.Strands).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class StrandConfiguration : IEntityTypeConfiguration<Strand>
{
    public void Configure(EntityTypeBuilder<Strand> builder)
    {
        builder.ToTable("strands");

        builder.Property<int>("Id");

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

        builder.Property<int>("Id");

        builder.HasKey("Id");

        builder.HasMany<ContentDescriptor>()
            .WithOne()
            .HasForeignKey("SubstrandId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(ss => ss.ContentDescriptors).Metadata.SetField("_contentDescriptors");
        builder.Navigation(ss => ss.ContentDescriptors).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class ContentDescriptorConfiguration : IEntityTypeConfiguration<ContentDescriptor>
{
    public void Configure(EntityTypeBuilder<ContentDescriptor> builder)
    {
        builder.ToTable("content_descriptors");

        builder.Property<int>("Id");

        builder.HasKey("Id");

        builder.HasMany<Elaboration>()
            .WithOne()
            .HasForeignKey("ContentDescriptorId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(cd => cd.Elaborations).Metadata.SetField("_elaborations");
        builder.Navigation(cd => cd.Elaborations).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class ElaborationConfiguration : IEntityTypeConfiguration<Elaboration>
{
    public void Configure(EntityTypeBuilder<Elaboration> builder)
    {
        builder.ToTable("elaborations");

        builder.Property<int>("Id");

        builder.HasKey("Id");
    }
}
