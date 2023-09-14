using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Application.Teachers.Commands.CreateTeacher;
using TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
using TeachPlanner.Contracts.Teacher.SetSubjectsTaught;
using TeachPlanner.Application.WeekPlanners.Queries.GetPlannerData;

namespace TeachPlanner.Api.Controllers;

[Route("api/[controller]")]
public class TeacherController : ApiController
{
    private readonly ISender _mediator;
    private readonly IValidator<CreateTeacherCommand> _validator;
    private readonly IMapper _mapper;

    public TeacherController(ISender mediator, IValidator<CreateTeacherCommand> validator, IMapper mapper)
    {
        _mediator = mediator;
        _validator = validator;
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
        var command = new SetSubjectsTaughtCommand(Guid.Parse(teacherId), request.SubjectNames);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
