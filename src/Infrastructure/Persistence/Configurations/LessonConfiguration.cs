using Domain.LessonAggregate;
using Domain.LessonAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        ConfigureLessonTable(builder);
        ConfigureCommentTable(builder);
        ConfigureLessonResourceIdsTable(builder);
        ConfigureLessonAssessmentIdsTable(builder);
    }

    private void ConfigureLessonAssessmentIdsTable(EntityTypeBuilder<Lesson> builder)
    {
        builder.OwnsMany(l => l.AssessmentIds, aib =>
        {
            aib.ToTable("LessonAssessmentIds");

            aib.WithOwner().HasForeignKey("LessonId");

            aib.HasKey("Id");

            aib.Property(a => a.Value)
                .HasColumnName("AssessmentId")
                .ValueGeneratedNever();
        });
    }

    private void ConfigureLessonResourceIdsTable(EntityTypeBuilder<Lesson> builder)
    {
        builder.OwnsMany(l => l.ResourceIds, rib =>
        {
            rib.ToTable("LessonResourceIds");

            rib.WithOwner().HasForeignKey("LessonId");

            rib.HasKey("Id");

            rib.Property(r => r.Value)
                .HasColumnName("ResourceId")
                .ValueGeneratedNever();
        });
    }

    private void ConfigureCommentTable(EntityTypeBuilder<Lesson> builder)
    {
        builder.OwnsMany(l => l.Comments, cb =>
        {
            cb.ToTable("LessonComments");

            cb.WithOwner().HasForeignKey("LessonId");

            cb.HasKey("Id", "LessonId");

            cb.Property(c => c.Id)
                .HasColumnName("CommentId")
                .HasConversion(id => id.Value, value => new CommentId(value));


        });
    }

    private void ConfigureLessonTable(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasConversion(l => l.Value, value => new LessonId(value))
            .ValueGeneratedNever();

        builder.Property(l => l.TeacherId)
            .HasConversion(t => t.Value, value => new TeacherId(value));
    }
}
