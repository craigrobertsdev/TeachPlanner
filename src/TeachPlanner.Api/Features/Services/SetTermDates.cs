using MediatR;
using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Contracts.Teachers.AccountSetup;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Features.Services;

public static class SetTermDates
{   
    public record Command(List<TermDate> TermDates) : IRequest;

    public sealed class Handler : IRequestHandler<Command>
    {
        private readonly ITermDateService _termDateService;
        private readonly ApplicationDbContext _context;

        public Handler(ITermDateService termDateService, ApplicationDbContext context)
        {
            _termDateService = termDateService;
            _context = context;
        }

        // adding term dates to the database
        // do I bother adding a column that tracks the calendar year?
        // should do a check if the term dates already exist for the year
        // if so, do I want to overwrite or throw an error?
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var termDates = _context.TermDates.Where(td => td.StartDate.Year == request.TermDates[0].StartDate.Year).ToList();

            if (termDates.Count == 0)
            {
               _context.TermDates.AddRange(request.TermDates);
            }
            else
            {
               _context.TermDates.RemoveRange(termDates);
               _context.TermDates.AddRange(request.TermDates);
            }

            await _context.SaveChangesAsync(cancellationToken);

            _termDateService.SetTermDates(request.TermDates);
        }
    }

    public static async Task<IResult> Delegate(List<TermDateDto> termDateDtos, ISender sender)
    {
        var termDates = termDateDtos.Select((td, i) => new TermDate(i+1, DateOnly.Parse(td.StartDate), DateOnly.Parse(td.EndDate))).ToList();
        var command = new Command(termDates);

        await sender.Send(command);

        return Results.Ok();
    }
}
