using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.GetTeacherSettings;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Features.Teachers;

public static class GetTeacherSettings
{
    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetTeacherSettingsResponse>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.TeacherId).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Query, GetTeacherSettingsResponse>
    {
        ApplicationDbContext _context;
        IUnitOfWork _unitOfWork;

        public Handler(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetTeacherSettingsResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers
                .Where(t => t.Id == request.TeacherId)
                .Include(t => t.Resources)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            var yearData = await _context.YearData
                .Where(yd => yd.TeacherId == request.TeacherId)
                .Where(yd => yd.CalendarYear == request.CalendarYear)
                .Include(yd => yd.Subjects)
                .Include(yd => yd.Students)
                .Include(yd => yd.YearLevelsTaught)
                .Include(yd => yd.WeekPlanners)
                .Include(yd => yd.LessonPlans)
                .Include(yd => yd.Reports)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (yearData == null)
            {
                yearData = YearData.Create(request.TeacherId, request.CalendarYear);
            }

            var termPlanner = await _context.GetTermPlanner(yearData.Id, request.CalendarYear, cancellationToken);

            if (termPlanner is null)
            {
                termPlanner = TermPlanner.Create(yearData.Id, request.CalendarYear, yearData.YearLevelsTaught.ToList());
            }

            return new GetTeacherSettingsResponse(
                yearData.Id,
                yearData.Subjects,
                yearData.Students,
                yearData.YearLevelsTaught,
                termPlanner);
        }

    }
    public async static Task<GetTeacherSettingsResponse> Delegate(Guid teacherId, int calendarYear, ISender sender)
    {
        var query = new Query(new TeacherId(teacherId), calendarYear);
        return await sender.Send(query);
    }
}
