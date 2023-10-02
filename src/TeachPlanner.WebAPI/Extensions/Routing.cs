using TeachPlanner.Api.Features.Authentication;
using TeachPlanner.Api.Features.Curriculum;
using TeachPlanner.Api.Features.LessonPlanners;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.Features.TermPlanners;
using TeachPlanner.Api.Features.YearDataRecords;

namespace TeachPlanner.Api.Extensions;

public static class Routing
{
    public static void MapApi(this WebApplication app)
    {
        var routePrefix = app.MapGroup("/api");
        var teacherPrefix = routePrefix.MapGroup("/{teacherId}");

        routePrefix
            .MapAuth();

        teacherPrefix
            .MapLessonPlans()
            .MapSubjects()
            .MapTeachers()
            .MapAssessments()
            .MapResources()
            .MapStudents()
            .MapTermPlanners()
            .MapWeekPlanners()
            .MapYearData()
            .RequireAuthorization();

        routePrefix
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
        var lessonPlanGroup = group.MapGroup("/lesson-plans");
        lessonPlanGroup.MapPost("/", CreateLessonPlan.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapSubjects(this RouteGroupBuilder group)
    {
        var subjectGroup = group.MapGroup("/subjects");

        return group;
    }

    private static RouteGroupBuilder MapTeachers(this RouteGroupBuilder group)
    {
        var teacherGroup = group.MapGroup("/teachers");
        teacherGroup.MapGet("/settings", GetTeacherSettings.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapAssessments(this RouteGroupBuilder group)
    {
        var assessmentGroup = group.MapGroup("/assessments");
        return group;
    }

    private static RouteGroupBuilder MapResources(this RouteGroupBuilder group)
    {
        var resourceGroup = group.MapGroup("/resources");
        return group;
    }

    private static RouteGroupBuilder MapStudents(this RouteGroupBuilder group)
    {
        var studentGroup = group.MapGroup("/students");
        return group;
    }

    private static RouteGroupBuilder MapTermPlanners(this RouteGroupBuilder group)
    {
        var termPlannerGroup = group.MapGroup("/term-planners");
        termPlannerGroup.MapPost("/", CreateTermPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapWeekPlanners(this RouteGroupBuilder group)
    {
        var weekPlannerGroup = group.MapGroup("/week-planners");
        return group;
    }

    private static RouteGroupBuilder MapYearData(this RouteGroupBuilder group)
    {
        var yearDataGroup = group.MapGroup("/year-data");
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
