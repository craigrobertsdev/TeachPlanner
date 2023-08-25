using Application.Curriculum.Commands.ParseCurriculum;
using Application.Curriculum.Queries.ListSubjects;
using Contracts.Curriculum;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

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
    public async Task<IActionResult> GetSubjects([FromQuery] bool elaborations = true)
    {
        var result = await _mediator.Send(new GetSubjectsQuery(elaborations));

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
