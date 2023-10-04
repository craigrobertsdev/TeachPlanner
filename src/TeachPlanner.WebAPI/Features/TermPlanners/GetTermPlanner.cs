using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Contracts.TermPlanners.GetTermPlanner;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.TermPlanners;

public static class GetTermPlanner
{
    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetTermPlannerResponse>;

    public class Handler : IRequestHandler<Query, GetTermPlannerResponse>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetTermPlannerResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _context.GetTeacherById(request.TeacherId, cancellationToken);

            if (teacher is null)
            {
                throw new TeacherNotFoundException();
            }

            var yearDataId = teacher.GetYearData(request.CalendarYear);

            if (yearDataId is null)
            {
                throw new YearDataNotFoundException();
            }

            var termPlanner = await _context.GetTermPlanner(yearDataId, request.CalendarYear, cancellationToken);

            if (termPlanner is null)
            {
                throw new TermPlannerNotFoundException();
            }

            return new GetTermPlannerResponse(termPlanner, new List<Subject>());
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, int calendarYear, ISender sender, CancellationToken cancellationToken)
    {
        var query = new Query(new TeacherId(teacherId), calendarYear);

        var result = await sender.Send(query);

        return Results.Ok(result);
    }
}