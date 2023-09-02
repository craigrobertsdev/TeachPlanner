using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachPlanner.Domain.Assessments;

namespace TeachPlanner.Infrastructure.Persistence.Configurations;
public class FormativeAssessmentConfiguration : IEntityTypeConfiguration<FormativeAssessment>
{
    public void Configure(EntityTypeBuilder<FormativeAssessment> builder)
    {
        builder.ToTable("formative_assessments");
    }
}
