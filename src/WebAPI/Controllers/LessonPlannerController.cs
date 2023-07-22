using Application.LessonPlan.CreateLessonPlan.Commands;
using Contracts.Plannner;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("teachers/{teacherId}/lessonplanner")]
public class LessonPlannerController : ApiController
{
    private readonly ISender _sender;
    private readonly IValidator<CreateLessonPlanCommand> _createLessonPlanValidator;
    private readonly IMapper _mapper;

    public LessonPlannerController(ISender sender, IValidator<CreateLessonPlanCommand> createLessonPlanValidator, IMapper mapper)
    {
        _sender = sender;
        _createLessonPlanValidator = createLessonPlanValidator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLessonPlan(CreateLessonPlanRequest request, string teacherId)
    {
        var command = _mapper.Map<CreateLessonPlanCommand>((request, teacherId));

        var validationResult = _createLessonPlanValidator.Validate(command);

        var createLessonPlanResult = await _sender.Send(command);

        return createLessonPlanResult.Match(
            lessonPlanResult => Ok(lessonPlanResult),
            errors => Problem(errors));
    }

}
