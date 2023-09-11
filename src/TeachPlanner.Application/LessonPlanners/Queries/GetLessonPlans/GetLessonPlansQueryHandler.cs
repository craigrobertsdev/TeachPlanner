using MediatR;
using TeachPlanner.Application.Common.Errors;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlanners.Queries.GetLessonPlans;
public class GetLessonPlansQueryHandler : IRequestHandler<GetLessonPlansQuery, List<LessonPlan>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILessonRepository _lessonRepository;

    public GetLessonPlansQueryHandler(ITeacherRepository teacherRepository, ILessonRepository lessonRepository)
    {
        _teacherRepository = teacherRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<List<LessonPlan>> Handle(GetLessonPlansQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherById(request.TeacherId);

        if (teacher is null)
        {
            throw new TeacherNotFoundException();
        }

        var lessonPlans = await _lessonRepository.GetLessonsByTeacherIdAsync(request.TeacherId);

        return lessonPlans;
    }
}
