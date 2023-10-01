using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.YearDataRecords;

public static class SetSubjectsTaught
{
    public record Command(
        TeacherId TeacherId,
        List<SubjectId> SubjectIds,
        int CalendarYear
        ) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.TeacherId).NotEmpty();
            RuleFor(x => x.SubjectIds).NotEmpty();
            RuleFor(x => x.CalendarYear).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var yearPlanner = await _context.YearData
                .Where(yd => yd.TeacherId == request.TeacherId)
                .Where(yd => yd.CalendarYear == request.CalendarYear)
                .Include(yd => yd.Subjects)
                .FirstOrDefaultAsync(cancellationToken);

            if (yearPlanner == null)
            {
                throw new YearDataNotFoundException();
            }

            var subjects = await _context.Subjects
                .Where(s => request.SubjectIds.Contains(s.Id))
                .ToListAsync(cancellationToken);

            if (subjects.Count != request.SubjectIds.Count)
            {
                throw new SubjectNotFoundException();
            }

            yearPlanner.AddSubjects(subjects);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async static Task<IResult> Delegate(Guid teacherId, ISender sender, CancellationToken cancellationToken)
    {
        var command = new Command(
            new TeacherId(teacherId),
            new List<SubjectId>(),
            DateTime.Now.Year);

        await sender.Send(command, cancellationToken);

        return Results.Ok();
    }
}
