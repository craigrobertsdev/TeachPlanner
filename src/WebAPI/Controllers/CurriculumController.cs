using TeachPlanner.Application.Curriculum.Commands.ParseCurriculum;
using TeachPlanner.Application.Curriculum.Queries.GetSubjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Contracts.Curriculum;

namespace TeachPlanner.Api.Controllers;

[Route("[controller]")]
public class CurriculumController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CurriculumController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjects(string teacherId, bool elaborations = true, bool taughtSubjectsOnly = false)
    {
        var result = await _mediator.Send(new GetSubjectsQuery(Guid.Parse(teacherId), elaborations, taughtSubjectsOnly));

        return result.Match(
            success => Ok(_mapper.Map<GetSubjectsResponse>(success)),
            errors => Problem(errors));
    }

    [HttpGet("ParseCurriculum")]
    public async Task<IActionResult> ParseCurriculum()
    {
        var parseCurriculumCommand = new ParseCurriculumCommand();
        var parseCurriculumResult = await _mediator.Send(parseCurriculumCommand);

        return parseCurriculumResult.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
