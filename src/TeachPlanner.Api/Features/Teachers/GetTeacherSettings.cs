using FluentValidation;
using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.Teachers.GetTeacherSettings;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Api.Features.Teachers;

public static class GetTeacherSettings {
    public static async Task<IResult> Delegate(Guid teacherId, int calendarYear, ISender sender,
        CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), calendarYear);
        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }

    public record Query(TeacherId TeacherId, int CalendarYear) : IRequest<GetTeacherSettingsResponse>;

    public class Validator : AbstractValidator<Query> {
        public Validator() {
            RuleFor(x => x.TeacherId).NotEmpty();
        }
    }

    public sealed class Handler : IRequestHandler<Query, GetTeacherSettingsResponse> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITermPlannerRepository _termPlannerRepository;
        private readonly IYearDataRepository _yearDataRepository;

        public Handler(ITeacherRepository teacherRepository, IYearDataRepository yearDataRepository,
            ITermPlannerRepository termPlannerRepository) {
            _teacherRepository = teacherRepository;
            _yearDataRepository = yearDataRepository;
            _termPlannerRepository = termPlannerRepository;
        }

        public async Task<GetTeacherSettingsResponse> Handle(Query request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
            if (teacher == null) throw new TeacherNotFoundException();

            var yearData =
                await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.CalendarYear,
                    cancellationToken);

            if (yearData == null) {
                var dayPlanTemplate = DayPlanTemplate.Create(new(), request.TeacherId);
                yearData = YearData.Create(request.TeacherId, request.CalendarYear, dayPlanTemplate);
            }

            var termPlanner =
                await _termPlannerRepository.GetByYearDataIdAndYear(yearData.Id, request.CalendarYear,
                    cancellationToken);

            if (termPlanner is null)
                termPlanner = TermPlanner.Create(yearData.Id, request.CalendarYear, yearData.YearLevelsTaught.ToList());

            return new GetTeacherSettingsResponse(
                yearData.Id,
                yearData.Subjects,
                yearData.Students,
                yearData.YearLevelsTaught,
                termPlanner);
        }
    }
}