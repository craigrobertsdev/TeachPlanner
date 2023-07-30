
using Domain.Assessments.ValueObjects;

namespace Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Assessment
    {
        public static AssessmentId Id = AssessmentId.Create();
        public const string Name = "Assessment Name";
        public const string Description = "Resource Description";
        public const string Url = "https://www.google.com";
    }
}
