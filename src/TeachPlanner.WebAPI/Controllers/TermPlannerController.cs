using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Contracts.TermPlanner.CreateTermPlanner;
using TeachPlanner.Api.Features.TermPlanners.Commands.CreateTermPlanner;
using TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
using TeachPlanner.Contracts.TermPlanner.CreateTermPlanner;
using TeachPlanner.Contracts.TermPlanner.GetTermPlanner;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Api.Controllers;

[Route("api/teacher/{teacherId}/term-planner")]
public class TermPlannerController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTermPlannerCommand> _createTermPlanValidator;

    public TermPlannerController(ISender mediator, IMapper mapper, IValidator<CreateTermPlannerCommand> createTermPlanValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _createTermPlanValidator = createTermPlanValidator;
    }

    [HttpGet]
    [Route("{calendarYear}")]
    public async Task<IActionResult> GetTermPlanner(
        string teacherId, int calendarYear, ISender sender, CancellationToken cancellationToken)
    {
        var command = new GetTermPlannerQuery(new TeacherId(Guid.Parse(teacherId)), calendarYear);

        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTermPlanner(
               CreateTermPlannerRequest request, string teacherId, ISender sender, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTermPlannerCommand>((request, teacherId));

        var validationResult = _createTermPlanValidator.Validate(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(); // TODO: return validation errors
        }

        await sender.Send(command);

        return Ok();
    }
}
