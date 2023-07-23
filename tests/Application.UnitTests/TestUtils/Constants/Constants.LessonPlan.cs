namespace Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class LessonPlan
    {
        public const string PlanningNotes = "Planning Notes";
        public static DateTime StartTime = DateTime.UtcNow;
        public static DateTime EndTime = DateTime.UtcNow.AddHours(1);
    }
}
