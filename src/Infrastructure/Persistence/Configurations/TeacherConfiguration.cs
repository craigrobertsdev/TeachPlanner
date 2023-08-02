using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ReportAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("teachers");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id");

        builder.HasMany<Subject>()
            .WithMany();

        builder.HasMany<Student>()
            .WithOne()
            .HasForeignKey(s => s.TeacherId);

        builder.HasMany<SummativeAssessment>()
            .WithOne()
            .HasForeignKey(sa => sa.TeacherId)
            .IsRequired();

        builder.HasMany<FormativeAssessment>()
            .WithOne()
            .HasForeignKey(fa => fa.TeacherId)
            .IsRequired();

        builder.HasMany<Report>()
            .WithOne()
            .HasForeignKey(r => r.TeacherId)
            .IsRequired();

        builder.HasMany<LessonPlan>()
           .WithOne()
           .HasForeignKey(lp => lp.TeacherId);

        /*        builder.Navigation(t => t.SubjectIds).Metadata.SetField("_subjectIds");
                builder.Navigation(t => t.SubjectIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(t => t.StudentIds).Metadata.SetField("_studentIds");
                builder.Navigation(t => t.StudentIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(t => t.SummativeAssessmentIds).Metadata.SetField("_summativeAssessmentIds");
                builder.Navigation(t => t.SummativeAssessmentIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(t => t.FormativeAssessmentIds).Metadata.SetField("_formativeAssessmentIds");
                builder.Navigation(t => t.FormativeAssessmentIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(t => t.ReportIds).Metadata.SetField("_reportIds");
                builder.Navigation(t => t.ReportIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

                builder.Navigation(t => t.LessonPlanIds).Metadata.SetField("_lessonPlanIds");
                builder.Navigation(t => t.LessonPlanIds).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
        */
    }
}
