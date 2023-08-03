using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.ReportAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Domain.UserAggregate;
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
    }
}
