using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Contracts.Teachers.SetSubjectsTaught;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Features.Teachers.Queries.GetTeacherSettings;
using TeachPlanner.Api.Features.WeekPlanners.Queries.GetWeekPlannerData;
using TeachPlanner.Api.Features.YearDataRecords.Commands.SetSubjectsTaught;

namespace TeachPlanner.Api.Controllers;

[Route("api/[controller]")]
public class TeacherController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TeacherController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("{teacherId}/planner")]
    public async Task<IActionResult> GetLessonPlannerData(string teacherId, CancellationToken cancellationToken)
    {
        var query = new GetPlannerDataQuery(teacherId);

        var lessonPlans = await _mediator.Send(query, cancellationToken);

        return Ok(lessonPlans);
    }

    [HttpPost]
    [Route("{teacherId}/subjects")]
    public async Task<IActionResult> SetSubjectsTaught(string teacherId, int calendarYear, SetSubjectsTaughtRequest request, CancellationToken cancellationToken)
    {
        var subjectIds = request.SubjectIds.Select(s => new SubjectId(Guid.Parse(s))).ToList();
        var command = new SetSubjectsTaughtCommand(new TeacherId(Guid.Parse(teacherId)), subjectIds, calendarYear);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet]
    [Route("{teacherId}/settings")]
    public async Task<IActionResult> GetTeacherSettings(string teacherId, int calendarYear)
    {
        var command = new GetTeacherSettingsQuery(new TeacherId(Guid.Parse(teacherId)), calendarYear);

        var settings = await _mediator.Send(command);

        return Ok(settings);
    }
}
