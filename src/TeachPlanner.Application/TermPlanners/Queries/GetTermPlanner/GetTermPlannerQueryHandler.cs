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
    private readonly IYearDataRepository _yearDataRepository;

    public GetTermPlannerQueryHandler(ITermPlannerRepository termPlannerRepository, ITeacherRepository teacherRepository, IYearDataRepository yearDataRepository)
    {
        _termPlannerRepository = termPlannerRepository;
        _teacherRepository = teacherRepository;
        _yearDataRepository = yearDataRepository;
    }
    public async Task<GetTermPlannerResult> Handle(GetTermPlannerQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

        if (teacher is null)
        {
            throw new TeacherNotFoundException();
        }

        var yearData = await _yearDataRepository.GetByTeacherAndYear(request.TeacherId, request.CalendarYear, cancellationToken);

        if (yearData.TermPlanner == null)
        {
            var termPlanner = TermPlanner.Create(teacher.Id, request.CalendarYear, new List<YearLevelValue>());
            yearData.AddTermPlanner(termPlanner);
        }

        return new GetTermPlannerResult(yearData.TermPlanner, new List<Subject>());
    }
}
