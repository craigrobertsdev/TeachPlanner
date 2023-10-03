using TeachPlanner.Api.Features.Authentication;
using TeachPlanner.Api.Features.Curriculum;
using TeachPlanner.Api.Features.LessonPlanners;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.Features.TermPlanners;
using TeachPlanner.Api.Features.YearDataRecords;

namespace TeachPlanner.Api.Extensions;

public static class RouteMapper
{
    public static void MapApi(this WebApplication app)
    {
        var authReqGroup = app.MapGroup("/api");
        var noAuthGroup = app.MapGroup("/api");

        noAuthGroup
            .MapAuth();

        authReqGroup
            .MapLessonPlans()
            .MapSubjects()
            .MapTeachers()
            .MapAssessments()
            .MapResources()
            .MapStudents()
            .MapTermPlanners()
            .MapWeekPlanners()
            .MapYearData()
            .MapCurriculum()
            .RequireAuthorization();
    }

    private static RouteGroupBuilder MapAuth(this RouteGroupBuilder group)
    {
        var authGroup = group.MapGroup("/auth");
        authGroup.MapPost("/register", Register.Delegate);
        authGroup.MapPost("/login", Login.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapLessonPlans(this RouteGroupBuilder group)
    {
        var lessonPlanGroup = group.MapGroup("/{teacherId}/lesson-plans");
        lessonPlanGroup.MapPost("/", CreateLessonPlan.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapSubjects(this RouteGroupBuilder group)
    {
        var subjectGroup = group.MapGroup("/{teacherId}/subjects");

        return group;
    }

    private static RouteGroupBuilder MapTeachers(this RouteGroupBuilder group)
    {
        var teacherGroup = group.MapGroup("/{teacherId}");
        teacherGroup.MapGet("/settings", GetTeacherSettings.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapAssessments(this RouteGroupBuilder group)
    {
        var assessmentGroup = group.MapGroup("/{teacherId}/assessments");
        return group;
    }

    private static RouteGroupBuilder MapResources(this RouteGroupBuilder group)
    {
        var resourceGroup = group.MapGroup("/{teacherId}/resources");
        return group;
    }

    private static RouteGroupBuilder MapStudents(this RouteGroupBuilder group)
    {
        var studentGroup = group.MapGroup("/{teacherId}/students");
        return group;
    }

    private static RouteGroupBuilder MapTermPlanners(this RouteGroupBuilder group)
    {
        var termPlannerGroup = group.MapGroup("/{teacherId}/term-planners");
        termPlannerGroup.MapPost("/", CreateTermPlanner.Delegate);
        termPlannerGroup.MapGet("/", GetTermPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapWeekPlanners(this RouteGroupBuilder group)
    {
        var weekPlannerGroup = group.MapGroup("/{teacherId}/week-planners");
        return group;
    }

    private static RouteGroupBuilder MapYearData(this RouteGroupBuilder group)
    {
        var yearDataGroup = group.MapGroup("/{teacherId}/year-data");
        yearDataGroup.MapPost("/set-subjects", SetSubjectsTaught.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapCurriculum(this RouteGroupBuilder group)
    {
        var curriculumGroup = group.MapGroup("/curriculum");

        curriculumGroup.MapPost("/parse-curriculum", ParseCurriculum.Delegate);

        return group;
    }
}
