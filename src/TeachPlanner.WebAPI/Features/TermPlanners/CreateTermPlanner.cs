using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.TermPlanners.CreateTermPlanner;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Features.TermPlanners;

public static class CreateTermPlanner
{
    public record Command(
        TeacherId TeacherId,
        List<TermPlan> TermPlans,
        List<YearLevelValue> YearLevels,
        int CalendarYear) : IRequest<CreateTermPlannerResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.TermPlans).NotEmpty();
            RuleFor(x => x.YearLevels).NotEmpty();
            RuleFor(x => x.CalendarYear).NotEmpty().GreaterThan(2022);
        }
    }

    internal sealed class Handler : IRequestHandler<Command, CreateTermPlannerResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateTermPlannerResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var yearData = await _context.YearData
                .Where(yd => yd.TeacherId == request.TeacherId)
                .Where(yd => yd.CalendarYear == request.CalendarYear)
                .SingleOrDefaultAsync(cancellationToken);

            if (yearData is null)
            {
                throw new YearDataNotFoundException();
            }

            var termPlanner = await _context.TermPlanners
                .Where(tp => tp.YearDataId == yearData.Id)
                .Where(tp => tp.CalendarYear == request.CalendarYear)
                .SingleOrDefaultAsync(cancellationToken);

            if (termPlanner is not null)
            {
                throw new TermPlannerAlreadyAssociatedException();
            }

            termPlanner = TermPlanner.Create(yearData.Id, request.CalendarYear, request.YearLevels);

            _context.TermPlanners.Add(termPlanner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateTermPlannerResponse(termPlanner.Id.Value);
        }
    }

    public async static Task<IResult> Delegate(Guid teacherId, int calendarYear, CreateTermPlannerRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new Command(
            new TeacherId(teacherId),
            request.TermPlans,
            request.YearLevels,
            calendarYear);

        var response = await sender.Send(command, cancellationToken);

        return Results.Ok(response);

    }
}
