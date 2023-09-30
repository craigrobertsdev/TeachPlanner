using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Contracts.LessonPlannner.CreateLessonPlan;
using TeachPlanner.Api.Contracts.LessonPlannner.GetLessonPlans;
using TeachPlanner.Api.Features.LessonPlanners.Commands.CreateLessonPlan;
using TeachPlanner.Api.Features.LessonPlanners.Queries.GetLessonPlans;
using TeachPlanner.Contracts.LessonPlannner.CreateLessonPlan;
using TeachPlanner.Contracts.LessonPlannner.GetLessonPlans;

namespace TeachPlanner.Api.Controllers;

[Route("api/teacher/{teacherId}/lesson-planner")]
public class LessonPlannerController : ApiController
{
    private readonly ISender _mediator;
    private readonly IValidator<CreateLessonPlanCommand> _createLessonPlanValidator;
    private readonly IMapper _mapper;

    public LessonPlannerController(ISender sender, IValidator<CreateLessonPlanCommand> createLessonPlanValidator, IMapper mapper)
    {
        _mediator = sender;
        _createLessonPlanValidator = createLessonPlanValidator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetLessonPlans(GetLessonPlansRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetLessonPlansQuery>(request);
        var lessonPlans = await _mediator.Send(query);

        return Ok(lessonPlans);

    }

    [HttpPost]
    public async Task<IActionResult> CreateLessonPlan(CreateLessonPlanRequest request, string teacherId, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateLessonPlanCommand>((request, teacherId));

        var validationResult = _createLessonPlanValidator.Validate(command);

        await _mediator.Send(command);

        return Ok();
    }

}
