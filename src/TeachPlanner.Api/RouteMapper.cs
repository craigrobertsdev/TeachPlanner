using TeachPlanner.Api.Features.Authentication;
using TeachPlanner.Api.Features.Curriculum;
using TeachPlanner.Api.Features.LessonPlans;
using TeachPlanner.Api.Features.Subjects;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.Features.TermPlanners;
using TeachPlanner.Api.Features.WeekPlanners;
using TeachPlanner.Api.Features.YearDataRecords;

namespace TeachPlanner.Api;

public static class RouteMapper
{
    public static void MapApi(this WebApplication app)
    {
        var authReqGroup = app.MapGroup("/api/{teacherId}");
        var noAuthGroup = app.MapGroup("/api");

        noAuthGroup
            .MapAuth();

        authReqGroup
            .MapAssessments()
            .MapCurriculum()
            .MapLessonPlans()
            .MapStudents()
            .MapSubjects()
            .MapTeachers()
            .MapTermPlanners()
            .MapWeekPlanners()
            .MapYearData()
            .RequireAuthorization();
    }

    private static RouteGroupBuilder MapAuth(this RouteGroupBuilder group)
    {
        var authGroup = group.MapGroup("/auth");
        authGroup.MapPost("/register", Register.Delegate);
        authGroup.MapPost("/login", Login.Delegate);

        return group;
    }


    private static RouteGroupBuilder MapAssessments(this RouteGroupBuilder group)
    {
        var assessmentGroup = group.MapGroup("/assessments");
        return group;
    }

    private static RouteGroupBuilder MapCurriculum(this RouteGroupBuilder group)
    {
        var curriculumGroup = group.MapGroup("/curriculum");
        curriculumGroup.MapPost("/parse-curriculum", ParseCurriculum.Delegate);
        curriculumGroup.MapGet("/subject-names", GetCurriculumSubjectNames.Delegate);
        
        return group;
    }

    private static RouteGroupBuilder MapLessonPlans(this RouteGroupBuilder group)
    {
        var lessonPlanGroup = group.MapGroup("/lesson-plans");
        lessonPlanGroup.MapPost("/", CreateLessonPlan.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapStudents(this RouteGroupBuilder group)
    {
        var studentGroup = group.MapGroup("/students");
        return group;
    }

    private static RouteGroupBuilder MapSubjects(this RouteGroupBuilder group)
    {
        var subjectGroup = group.MapGroup("/subjects");
        subjectGroup.MapGet("/curriculum", GetCurriculumSubjects.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapTeachers(this RouteGroupBuilder group)
    {
        var teacherGroup = group.MapGroup("");
        teacherGroup.MapGet("/settings", GetTeacherSettings.Delegate);
        teacherGroup.MapGet("/resources/{subjectId}", GetResources.Delegate);
        teacherGroup.MapPost("/resources", CreateResource.Delegate);
        teacherGroup.MapPost("/setup", AccountSetup.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapTermPlanners(this RouteGroupBuilder group)
    {
        var termPlannerGroup = group.MapGroup("/term-planners");
        termPlannerGroup.MapPost("/", CreateTermPlanner.Delegate);
        termPlannerGroup.MapGet("/", GetTermPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapWeekPlanners(this RouteGroupBuilder group)
    {
        var weekPlannerGroup = group.MapGroup("/week-planner");
        weekPlannerGroup.MapGet("/", GetWeekPlanner.Delegate);
        weekPlannerGroup.MapPost("/", CreateWeekPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapYearData(this RouteGroupBuilder group)
    {
        var yearDataGroup = group.MapGroup("/year-data");
        yearDataGroup.MapPost("/set-subjects", SetSubjectsTaught.Delegate);
        return group;
    }
}