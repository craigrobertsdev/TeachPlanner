using TeachPlanner.Application.Curriculum.Commands.ParseCurriculum;
using TeachPlanner.Application.Curriculum.Queries.GetAllSubjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Contracts.Curriculum;
using Microsoft.AspNetCore.Authorization;
using TeachPlanner.Api.Identity;

namespace TeachPlanner.Api.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> GetSubjects(bool elaborations, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSubjectsQuery(elaborations));

        return Ok(_mapper.Map<GetSubjectsResponse>(result));
    }

    // [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    [HttpGet("ParseCurriculum")]
    public async Task<IActionResult> ParseCurriculum()
    {
        var parseCurriculumCommand = new ParseCurriculumCommand();
        await _mediator.Send(parseCurriculumCommand);

        return Ok();
    }
}
