using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlanners.Queries.GetLessonPlans;
public class GetLessonPlansQueryHandler : IRequestHandler<GetLessonPlansQuery, List<LessonPlan>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetLessonPlansQueryHandler(ITeacherRepository teacherRepository, ILessonRepository lessonRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _lessonRepository = lessonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<LessonPlan>> Handle(GetLessonPlansQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

        if (teacher is null)
        {
            throw new TeacherNotFoundException();
        }

        var lessonPlans = await _lessonRepository.GetLessonsByTeacherIdAsync(request.TeacherId, cancellationToken);

        if (lessonPlans == null)
        {
            throw new LessonPlansNotFoundException();
        }

        return lessonPlans;
    }
}
