using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Contracts.TermPlanners.GetTermPlanner;
using TeachPlanner.Shared.Common.Interfaces.Persistence;

namespace TeachPlanner.Blazor.Features.TermPlanners;

public static class GetTermPlanner {
    public static async Task<IResult> Delegate(Guid teacherId, int calendarYear, ISender sender,
        CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), calendarYear);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }

    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetTermPlannerResponse>;

    public class Handler : IRequestHandler<Query, GetTermPlannerResponse> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITermPlannerRepository _termPlannerRepository;

        public Handler(ITermPlannerRepository termPlannerRepository, ITeacherRepository teacherRepository) {
            _termPlannerRepository = termPlannerRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<GetTermPlannerResponse> Handle(Query request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null) throw new TeacherNotFoundException();

            var yearDataId = teacher.GetYearData(request.CalendarYear);

            if (yearDataId is null) throw new YearDataNotFoundException();

            var termPlanner =
                await _termPlannerRepository.GetByYearDataIdAndYear(yearDataId, request.CalendarYear,
                    cancellationToken);

            if (termPlanner is null) throw new TermPlannerNotFoundException();

            return new GetTermPlannerResponse(termPlanner);
        }
    }
}