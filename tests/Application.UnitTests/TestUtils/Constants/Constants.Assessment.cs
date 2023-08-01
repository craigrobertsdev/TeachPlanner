
using Domain.Assessments;

namespace Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Assessment
    {
        public static SummativeAssessmentId SummativeAssessmentId = new SummativeAssessmentId(Guid.NewGuid());
        public static FormativeAssessmentId FormativeAssessmentId = new FormativeAssessmentId(Guid.NewGuid());
        public const string Name = "Assessment Name";
        public const string Description = "Resource Description";
        public const string Url = "https://www.google.com";
    }
}
