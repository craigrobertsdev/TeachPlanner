using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Shared.Contracts.Teachers.AccountSetup;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Contracts.Services;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Common.Interfaces.Services;

namespace TeachPlanner.Blazor.Features.Services;

public static class SetTermDates {
    public record Command(List<TermDate> TermDates) : IRequest;

    public sealed class Handler : IRequestHandler<Command> {
        private readonly ITermDatesService _termDateService;
        private readonly ApplicationDbContext _context;

        public Handler(ITermDatesService termDateService, ApplicationDbContext context) {
            _termDateService = termDateService;
            _context = context;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken) {
            var termDates = _context.TermDates.Where(td => td.StartDate.Year == request.TermDates[0].StartDate.Year).ToList();

            if (termDates.Count == 0) {
                _context.TermDates.AddRange(request.TermDates);
            } else {
                _context.TermDates.RemoveRange(termDates);
                _context.TermDates.AddRange(request.TermDates);
            }

            await _context.SaveChangesAsync(cancellationToken);

            _termDateService.SetTermDates(request.TermDates);
        }
    }

    public static async Task<IResult> Delegate([FromBody] SetTermDatesRequest request, ISender sender) {
        var termDates = request.TermDateDtos.Select((td, i) => new TermDate(i + 1, DateOnly.Parse(td.StartDate), DateOnly.Parse(td.EndDate))).ToList();
        var command = new Command(termDates);

        await sender.Send(command);

        return Results.Ok();
    }
}
