using TeachPlanner.Blazor.Features.Curriculum;
using TeachPlanner.Blazor.Features.LessonPlans;
using TeachPlanner.Blazor.Features.Services;
using TeachPlanner.Blazor.Features.Subjects;
using TeachPlanner.Blazor.Features.Teachers;
using TeachPlanner.Blazor.Features.TermPlanners;
using TeachPlanner.Blazor.Features.WeekPlanners;
using TeachPlanner.Blazor.Features.YearDataRecords;

namespace TeachPlanner.Blazor;

public static class RouteMapper {
    public static void MapApi(this WebApplication app) {
        var authReqGroup = app.MapGroup("/api/{teacherId}");
        var noAuthGroup = app.MapGroup("/api");

        noAuthGroup
            //.MapAuth()
            .MapServices();

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

    private static RouteGroupBuilder MapAuth(this RouteGroupBuilder group) {
        var authGroup = group.MapGroup("/auth");
        //authGroup.MapPost("/register", Register.Delegate);
        //authGroup.MapPost("/login", Login.Delegate);

        return group;
    }


    private static RouteGroupBuilder MapAssessments(this RouteGroupBuilder group) {
        var assessmentGroup = group.MapGroup("/assessments");
        return group;
    }

    private static RouteGroupBuilder MapCurriculum(this RouteGroupBuilder group) {
        var curriculumGroup = group.MapGroup("/curriculum");
        curriculumGroup.MapGet("/subject-names", GetCurriculumSubjectNames.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapLessonPlans(this RouteGroupBuilder group) {
        var lessonPlanGroup = group.MapGroup("/lesson-plans");
        lessonPlanGroup.MapGet("/data", GetDataForBlankLessonPlan.Delegate);
        lessonPlanGroup.MapPost("/", CreateLessonPlan.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapServices(this RouteGroupBuilder group) {
        var serviceGroup = group.MapGroup("/services");
        serviceGroup.MapPost("/term-dates", SetTermDates.Delegate);
        serviceGroup.MapPost("/parse-curriculum", ParseCurriculum.Delegate);

        return serviceGroup;
    }

    private static RouteGroupBuilder MapStudents(this RouteGroupBuilder group) {
        var studentGroup = group.MapGroup("/students");
        return group;
    }

    private static RouteGroupBuilder MapSubjects(this RouteGroupBuilder group) {
        var subjectGroup = group.MapGroup("/subjects");
        subjectGroup.MapGet("/curriculum", GetCurriculumSubjects.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapTeachers(this RouteGroupBuilder group) {
        var teacherGroup = group.MapGroup("");
        teacherGroup.MapGet("/settings", GetTeacherSettings.Delegate);
        teacherGroup.MapGet("/resources/{subjectId}", GetResources.Delegate);
        teacherGroup.MapPost("/resources", CreateResource.Delegate);
        teacherGroup.MapPost("/setup", AccountSetup.Delegate);

        return group;
    }

    private static RouteGroupBuilder MapTermPlanners(this RouteGroupBuilder group) {
        var termPlannerGroup = group.MapGroup("/term-planners");
        termPlannerGroup.MapPost("/", CreateTermPlanner.Delegate);
        termPlannerGroup.MapGet("/", GetTermPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapWeekPlanners(this RouteGroupBuilder group) {
        var weekPlannerGroup = group.MapGroup("/week-planner");
        weekPlannerGroup.MapGet("/", GetWeekPlanner.Delegate);
        weekPlannerGroup.MapPatch("/", CreateWeekPlanner.Delegate);
        return group;
    }

    private static RouteGroupBuilder MapYearData(this RouteGroupBuilder group) {
        var yearDataGroup = group.MapGroup("/year-data");
        yearDataGroup.MapPost("/set-subjects", SetSubjectsTaught.Delegate);
        return group;
    }
}