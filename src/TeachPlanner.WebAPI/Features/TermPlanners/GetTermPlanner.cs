using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.TermPlanners.GetTermPlanner;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.TermPlanners;

public static class GetTermPlanner
{
    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetTermPlannerResponse>;

    public class Handler : IRequestHandler<Query, GetTermPlannerResponse>
    {
        private readonly ITermPlannerRepository _termPlannerRepository;
        private readonly ITeacherRepository _teacherRepository;

        public Handler(ITermPlannerRepository termPlannerRepository, ITeacherRepository teacherRepository)
        {
            _termPlannerRepository = termPlannerRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<GetTermPlannerResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null)
            {
                throw new TeacherNotFoundException();
            }

            var yearDataId = teacher.GetYearData(request.CalendarYear);

            if (yearDataId is null)
            {
                throw new YearDataNotFoundException();
            }

            var termPlanner = await _termPlannerRepository.GetByYearDataIdAndYear(yearDataId, request.CalendarYear, cancellationToken);

            if (termPlanner is null)
            {
                throw new TermPlannerNotFoundException();
            }

            return new GetTermPlannerResponse(termPlanner);
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, int calendarYear, ISender sender, CancellationToken cancellationToken)
    {
        var query = new Query(new TeacherId(teacherId), calendarYear);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}