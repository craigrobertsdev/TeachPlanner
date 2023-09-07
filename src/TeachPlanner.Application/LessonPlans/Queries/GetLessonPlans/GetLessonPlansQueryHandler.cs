using ErrorOr;
using MediatR;
using TeachPlanner.Application.Common.Errors;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlans.Queries.GetLessonPlans;
public class GetLessonPlansQueryHandler : IRequestHandler<GetLessonPlansQuery, ErrorOr<List<LessonPlan>>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILessonRepository _lessonRepository;

    public GetLessonPlansQueryHandler(ITeacherRepository teacherRepository, ILessonRepository lessonRepository)
    {
        _teacherRepository = teacherRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<ErrorOr<List<LessonPlan>?>> Handle(GetLessonPlansQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherById(request.TeacherId);

        if (teacher is null)
        {
            return Errors.Teacher.TeacherNotFound;
        }

        var lessonPlans = await _lessonRepository.GetLessonsByTeacherIdAsync(request.TeacherId);

        return lessonPlans;
    }
}
