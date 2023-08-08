using Application.LessonPlan.CreateLessonPlan.Commands;
using Application.UnitTests.TestUtils.Constants;
using Domain.ResourceAggregate;

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
    public static CreateLessonPlanCommand CreateCommand(int numberOfPeriods, List<Resource>? resources = null, List<string>? summativeAssessmentIds = null, List<string>? formativeAssessmentIds = null) =>
        new CreateLessonPlanCommand(
            Constants.Teacher.Id.ToString()!,
            Constants.Subject.Id.ToString()!,
            Constants.LessonPlan.PlanningNotes,
            resources,
            summativeAssessmentIds != null ? CreateAssessmentIdList() : new List<string>(),
            formativeAssessmentIds != null ? CreateAssessmentIdList() : new List<string>(),
            Constants.LessonPlan.StartTime,
            Constants.LessonPlan.EndTime,
            numberOfPeriods);

    public static List<string> CreateAssessmentIdList(int count = 1) =>
        Enumerable.Range(0, count).Select(_ => Guid.NewGuid().ToString()).ToList();

    /*    public static List<Resource> CreateResourceIdList(int count = 1) =>
            Enumerable.Range(0, count).Select(_ => Guid.NewGuid().ToString()).ToList();
    */
}

