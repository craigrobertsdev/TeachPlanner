using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Features.Curriculum;

public static class ParseCurriculum
{
    public record Command() : IRequest;

    internal sealed class Handler : IRequestHandler<Command>
    {
        private readonly ICurriculumParser _curriculumParser;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICurriculumParser curriculumParser, IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _curriculumParser = curriculumParser;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var subjects = _curriculumParser.ParseCurriculum();

            await _context.SaveCurriculum(subjects, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task SaveCurriculum(this ApplicationDbContext context, List<Subject> subjects, CancellationToken cancellationToken)
    {
        foreach (var subject in subjects)
        {
            if (!subject.IsCurriculumSubject)
            {
                throw new AttemptedToAddNonCurriculumSubjectException(subject.Name);
            }
        }

        // clear existing curriculum subjects
        var curriculumSubjects = await context.Subjects
            .Where(s => s.IsCurriculumSubject)
            .ToListAsync(cancellationToken);

        context.Subjects.RemoveRange(curriculumSubjects);
        await context.SaveChangesAsync(cancellationToken);

        // add new subjects
        foreach (var subject in subjects)
        {
            context.Subjects.Add(subject);
        }
    }

    public static async Task<IResult> Delegate(ISender sender, CancellationToken cancellationToken)
    {
        var command = new Command();
        await sender.Send(command, cancellationToken);

        return Results.Ok();
    }
}

