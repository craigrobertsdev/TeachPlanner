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

    public sealed class Handler : IRequestHandler<Query, GetTeacherSettingsResponse>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IYearDataRepository _yearDataRepository;
        private readonly ITermPlannerRepository _termPlannerRepository;

        public Handler(ITeacherRepository teacherRepository, IYearDataRepository yearDataRepository, ITermPlannerRepository termPlannerRepository)
        {
            _teacherRepository = teacherRepository;
            _yearDataRepository = yearDataRepository;
            _termPlannerRepository = termPlannerRepository;
        }

        public async Task<GetTeacherSettingsResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.CalendarYear, cancellationToken);

            if (yearData == null)
            {
                yearData = YearData.Create(request.TeacherId, request.CalendarYear);
            }

            var termPlanner = await _termPlannerRepository.GetByYearDataIdAndYear(yearData.Id, request.CalendarYear, cancellationToken);

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
    public async static Task<GetTeacherSettingsResponse> Delegate(Guid teacherId, int calendarYear, ISender sender, CancellationToken cancellationToken)
    {
        var query = new Query(new TeacherId(teacherId), calendarYear);
        return await sender.Send(query, cancellationToken);
    }
}
