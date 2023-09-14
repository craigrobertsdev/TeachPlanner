namespace Application.UnitTests.TestUtils.Constants;
public static partial class Constants
{
    public static class Assessment
    {
        public static Guid SummativeAssessmentId = Guid.NewGuid();
        public static Guid FormativeAssessmentId = Guid.NewGuid();
        public const string Name = "Assessment Name";
        public const string Description = "Resource Description";
        public const string Url = "https://www.google.com";
    }
}
