using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;

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

        builder.HasMany(t => t.StudentsForYear)
            .WithOne();

        builder.HasMany(t => t.SummativeAssessments)
            .WithOne()
            .IsRequired();

        builder.HasMany(t => t.FormativeAssessments)
            .WithOne()
            .IsRequired();

        builder.HasMany(t => t.Reports)
            .WithOne()
            .HasForeignKey(r => r.TeacherId)
            .IsRequired();

        builder.HasMany(t => t.LessonPlans)
           .WithOne()
           .HasForeignKey(lp => lp.TeacherId);

        builder.HasMany(t => t.TermPlanners)
            .WithOne()
            .HasForeignKey(tp => tp.TeacherId);
    }
}
