using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
using TeachPlanner.Contracts.Teacher.SetSubjectsTaught;
using TeachPlanner.Application.WeekPlanners.Queries.GetPlannerData;
using TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;

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
    public async Task<IActionResult> SetSubjectsTaught(string teacherId, SetSubjectsTaughtRequest request, CancellationToken cancellationToken)
    {
        var command = new SetSubjectsTaughtCommand(Guid.Parse(teacherId), request.SubjectIds, 2023);

        await _mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    [Route("{teacherId}/settings")]
    public async Task<IActionResult> GetTeacherSettings(string teacherId, int calendarYear)
    {
        var command = new GetTeacherSettingsQuery(Guid.Parse(teacherId), calendarYear);

        var settings = await _mediator.Send(command);

        return Ok(settings);
    }
}
