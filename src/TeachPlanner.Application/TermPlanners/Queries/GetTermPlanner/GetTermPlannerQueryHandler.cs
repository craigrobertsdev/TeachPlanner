using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public class GetTermPlannerQueryHandler : IRequestHandler<GetTermPlannerQuery, GetTermPlannerResult>
{
    private readonly ITermPlannerRepository _termPlannerRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetTermPlannerQueryHandler(ITermPlannerRepository termPlannerRepository, ITeacherRepository teacherRepository)
    {
        _termPlannerRepository = termPlannerRepository;
        _teacherRepository = teacherRepository;
    }
    public async Task<GetTermPlannerResult> Handle(GetTermPlannerQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

        if (teacher is null)
        {
            throw new TeacherNotFoundException();
        }

        var termPlanner = teacher.TermPlanners.SingleOrDefault(tp => tp.CalendarYear == request.CalendarYear);

        if (termPlanner == null)
        {
            TermPlanner.Create(teacher.Id, request.CalendarYear, new List<YearLevelValue>());
        }

        if (termPlanner.TeacherId != request.TeacherId)
        {
            throw new TermPlannerDoesNotBelongToTeacherException();
        }

        return new GetTermPlannerResult(termPlanner, new List<Subject>());
    }
}
