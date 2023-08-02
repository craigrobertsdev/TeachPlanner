using Domain.Assessments;
using Domain.LessonPlanAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class FormativeAssessmentConfiguration : IEntityTypeConfiguration<FormativeAssessment>
{
    public void Configure(EntityTypeBuilder<FormativeAssessment> builder)
    {
        builder.ToTable("formative_assessments");
    }
}
