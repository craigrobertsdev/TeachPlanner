using Application.LessonPlan.CreateLessonPlan.Commands;
using Contracts.Plannner;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("teacher/{teacherId}/lessonplanner")]
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

    [HttpPost]
    public async Task<IActionResult> CreateLessonPlan(CreateLessonPlanRequest request, string teacherId)
    {
        var command = _mapper.Map<CreateLessonPlanCommand>((request, teacherId));

        var validationResult = _createLessonPlanValidator.Validate(command);

        var createLessonPlanResult = await _mediator.Send(command);

        return createLessonPlanResult.Match(
            lessonPlanResult => Ok(lessonPlanResult),
            errors => Problem(errors));
    }

}
