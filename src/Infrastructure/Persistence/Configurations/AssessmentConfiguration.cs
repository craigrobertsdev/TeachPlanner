using Domain.Assessments;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class AssessmentConfiguration : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("Id");

        builder.HasOne<Teacher>()
            .WithMany()
            .HasForeignKey(fa => fa.TeacherId)
            .IsRequired();

        builder.HasOne<Subject>()
            .WithMany()
            .HasForeignKey(fa => fa.SubjectId)
            .IsRequired();

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(fa => fa.StudentId)
            .IsRequired();

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();
    }
}
