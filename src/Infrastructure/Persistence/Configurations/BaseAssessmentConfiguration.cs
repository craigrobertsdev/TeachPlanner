using Domain.Assessments;
using Domain.Assessments.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BaseAssessmentConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Assessment
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => new AssessmentId(id));

        builder.Property(a => a.TeacherId)
            .HasConversion(id => id.Value, id => new TeacherId(id));

        builder.Property(a => a.SubjectId)
            .HasConversion(id => id.Value, id => new SubjectId(id));

        builder.Property(a => a.StudentId)
            .HasConversion(id => id.Value, id => new StudentId(id));

        builder.Property(a => a.YearLevel)
            .HasConversion<string>();
    }
}
