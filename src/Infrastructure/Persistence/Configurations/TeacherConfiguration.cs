using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ReportAggregate;
using Domain.ResourceAggregate;
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
            .HasConversion(
                v => v.Value,
                v => new TeacherId(v))
            .ValueGeneratedNever();

        builder.HasMany<Subject>()
            .WithMany();

        builder.HasMany<Student>()
            .WithOne()
            .HasForeignKey(s => s.TeacherId);

        builder.OwnsMany(t => t.SummativeAssessmentIds, sib =>
        {
            sib.WithOwner().HasForeignKey("TeacherId");

            sib.ToTable("teacher_summative_assessment");
        });

        builder.OwnsMany(t => t.FormativeAssessmentIds, fib =>
        {
            fib.WithOwner().HasForeignKey("TeacherId");

            fib.ToTable("teacher_formative_assessment");
        });

        builder.OwnsMany(t => t.ResourceIds, rib =>
        {
            rib.WithOwner().HasForeignKey("TeacherId");

            rib.ToTable("teacher_resource");
        });

        builder.OwnsMany(t => t.ReportIds, rib =>
        {
            rib.WithOwner().HasForeignKey("TeacherId");

            rib.ToTable("teacher_report");
        });

        builder.HasMany<LessonPlan>()
           .WithOne()
           .HasForeignKey(lp => lp.TeacherId);


    }
}
