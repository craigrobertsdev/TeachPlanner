using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LessonPlanConfiguration : IEntityTypeConfiguration<LessonPlan>
{
    public void Configure(EntityTypeBuilder<LessonPlan> builder)
    {
        builder.ToTable("lesson_plans");

        builder.HasKey(lp => lp.Id);

        builder.Property(lp => lp.Id)
            .HasColumnName("Id");

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(lp => lp.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(lp => lp.SubjectId)
            .IsRequired();

        builder.OwnsMany(lp => lp.Comments, lcb =>
        {
            lcb.ToTable("lesson_comment");

            lcb.WithOwner().HasForeignKey("LessonPlanId");

            lcb.Property<Guid>("Id");

            lcb.HasKey("Id", "LessonPlanId");
        });

        builder.HasMany<SummativeAssessment>()
            .WithOne()
            .HasForeignKey("LessonPlanId");

        builder.HasMany<FormativeAssessment>()
            .WithOne()
            .HasForeignKey("LessonPlanId");

        builder.HasMany<Resource>()
            .WithMany();

        /*        builder.Navigation(lp => lp.SummativeAssessmentIds).Metadata.SetField("_summativeAssessmentIds");
                builder.Navigation(lp => lp.SummativeAssessmentIds).UsePropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(lp => lp.FormativeAssessmentIds).Metadata.SetField("_formativeAssessmentIds");
                builder.Navigation(lp => lp.FormativeAssessmentIds).UsePropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(lp => lp.ResourceIds).Metadata.SetField("_resourceIds");
                builder.Navigation(lp => lp.ResourceIds).UsePropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(lp => lp.Comments).Metadata.SetField("_comments");
                builder.Navigation(lp => lp.Comments).UsePropertyAccessMode(PropertyAccessMode.Field);
        */
    }
}
