using FluentValidation;
using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.TermPlanners.CreateTermPlanner;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Features.TermPlanners;

public static class CreateTermPlanner {
    public static async Task<IResult> Delegate(Guid teacherId, int calendarYear, CreateTermPlannerRequest request,
        ISender sender, CancellationToken cancellationToken) {
        var command = new Command(
            new TeacherId(teacherId),
            request.TermPlans,
            request.YearLevels,
            calendarYear);

        var response = await sender.Send(command, cancellationToken);

        return Results.Ok(response);
    }

    public record Command(
        TeacherId TeacherId,
        List<TermPlan> TermPlans,
        List<YearLevelValue> YearLevels,
        int CalendarYear) : IRequest<CreateTermPlannerResponse>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(x => x.TermPlans).NotEmpty();
            RuleFor(x => x.YearLevels).NotEmpty();
            RuleFor(x => x.CalendarYear).NotEmpty().GreaterThan(2022);
        }
    }

    internal sealed class Handler : IRequestHandler<Command, CreateTermPlannerResponse> {
        private readonly ITermPlannerRepository _termPlannerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IYearDataRepository _yearDataRepository;

        public Handler(ITermPlannerRepository termPlannerRepository, IYearDataRepository yearDataRepository,
            IUnitOfWork unitOfWork) {
            _termPlannerRepository = termPlannerRepository;
            _yearDataRepository = yearDataRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateTermPlannerResponse> Handle(Command request, CancellationToken cancellationToken) {
            var yearData =
                await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.CalendarYear,
                    cancellationToken);
            if (yearData is null) throw new YearDataNotFoundException();

            var termPlanner =
                await _termPlannerRepository.GetByYearDataIdAndYear(yearData.Id, request.CalendarYear,
                    cancellationToken);
            if (termPlanner is not null) throw new TermPlannerAlreadyAssociatedException();

            termPlanner = TermPlanner.Create(yearData.Id, request.CalendarYear, request.YearLevels);

            _termPlannerRepository.Add(termPlanner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateTermPlannerResponse(termPlanner.Id.Value);
        }
    }
}