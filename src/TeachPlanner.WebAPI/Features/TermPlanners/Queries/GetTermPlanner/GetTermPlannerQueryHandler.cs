using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Features.TermPlanners.Queries.GetTermPlanner;
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

        if (yearData is null)
        {
            throw new YearDataNotFoundException();
        }

        if (yearData.TermPlanner == null)
        {
            var termPlanner = TermPlanner.Create(yearData.Id, request.CalendarYear, new List<YearLevelValue>());
            yearData.AddTermPlanner(termPlanner);
        }

        return new GetTermPlannerResult(yearData.TermPlanner!, new List<Subject>());
    }
}
