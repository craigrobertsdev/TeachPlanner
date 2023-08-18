using Application.Planner.Queries.GetPlannerData;
using Application.Teachers.Common;
using Application.Teachers.CreateTeacher.Commands;
using Contracts.Teacher;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
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
    public async Task<IActionResult> GetPlannerData(string teacherId)
    {
        var query = new GetPlannerDataQuery(teacherId);

        var lessonPlans = await _mediator.Send(query);

        return lessonPlans.Match(
            lessonPlans => Ok(lessonPlans),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeacher(CreateTeacherRequest request)
    {
        var command = _mapper.Map<CreateTeacherCommand>(request);

        var validationResult = _validator.Validate(command);

        var createdTeacherResult = await _mediator.Send(command);

        return createdTeacherResult.Match(
            createdTeacherResult => Ok(_mapper.Map<TeacherCreatedResult>(createdTeacherResult)),
            errors => Problem(errors));
    }
}
