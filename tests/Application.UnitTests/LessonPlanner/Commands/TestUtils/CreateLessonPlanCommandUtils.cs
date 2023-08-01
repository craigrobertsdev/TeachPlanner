using Application.LessonPlan.CreateLessonPlan.Commands;
using Application.UnitTests.TestUtils.Constants;

namespace Application.UnitTests.LessonPlanner.Commands.TestUtils;

public static class CreateLessonPlanCommandUtils
{
    // teacherId
    // subjectId
    // planningNotes
    // list of resourceIds
    // list of assessmentIds
    // startTime 
    // endTime
    public static CreateLessonPlanCommand CreateCommand(List<string>? resourceIds = null, List<string>? summativeAssessmentIds = null, List<string>? formativeAssessmentIds = null) =>
        new CreateLessonPlanCommand(
            Constants.Teacher.Id.ToString()!,
            Constants.Subject.Id.ToString()!,
            Constants.LessonPlan.PlanningNotes,
            resourceIds != null ? CreateResourceIdList() : new List<string>(),
            summativeAssessmentIds != null ? CreateAssessmentIdList() : new List<string>(),
            formativeAssessmentIds != null ? CreateAssessmentIdList() : new List<string>(),
            Constants.LessonPlan.StartTime,
            Constants.LessonPlan.EndTime);

    public static List<string> CreateAssessmentIdList(int count = 1) =>
        Enumerable.Range(0, count).Select(_ => Guid.NewGuid().ToString()).ToList();

    public static List<string> CreateResourceIdList(int count = 1) =>
        Enumerable.Range(0, count).Select(_ => Guid.NewGuid().ToString()).ToList();
}

