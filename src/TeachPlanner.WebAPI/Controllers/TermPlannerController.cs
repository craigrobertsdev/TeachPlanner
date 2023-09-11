using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Application.TermPlanners.Commands.CreateTermPlanner;
using TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
using TeachPlanner.Contracts.TermPlanner.CreateTermPlanner;
using TeachPlanner.Contracts.TermPlanner.GetTermPlanner;

namespace TeachPlanner.Api.Controllers;

[Route("teacher/{teacherId}/lesson-planner")]
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
    public async Task<IActionResult> GetTermPlanner(
        GetTermPlannerRequest request, ISender sender)
    {
        var command = new GetTermPlannerQuery(Guid.Parse(request.TeacherId), Guid.Parse(request.TermPlannerId));

        var response = await sender.Send(command);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTermPlanner(
               CreateTermPlannerRequest request, string teacherId, ISender sender)
    {
        var command = _mapper.Map<CreateTermPlannerCommand>((request, teacherId));

        var validationResult = _createTermPlanValidator.Validate(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(); // TODO: return validation errors
        }

        var response = await sender.Send(command);

        return Ok();
    }
}
