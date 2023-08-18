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

        builder.Property(t => t.FirstName).HasMaxLength(50);

        builder.Property(t => t.LastName).HasMaxLength(50);

        builder.Property(t => t.Email).HasMaxLength(255);

        builder.HasIndex(t => t.Email).IsUnique();

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
