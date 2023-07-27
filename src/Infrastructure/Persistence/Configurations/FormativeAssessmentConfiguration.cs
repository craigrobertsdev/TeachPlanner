using Domain.Assessments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class FormativeAssessmentConfiguration : BaseAssessmentConfiguration<FormativeAssessment>
{
    public override void Configure(EntityTypeBuilder<FormativeAssessment> builder)
    {
        base.Configure(builder);
    }
}
