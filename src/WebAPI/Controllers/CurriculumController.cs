using Application.Curriculum.ParseCurriculum;
using Infrastructure.Curriculum;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
    public IActionResult ListSubjects()
    {
        return Ok(Array.Empty<string>());
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
