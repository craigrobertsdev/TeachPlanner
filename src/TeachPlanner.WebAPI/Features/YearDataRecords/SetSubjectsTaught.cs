﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext context, IUnitOfWork unitOfWork, IValidator<Command> validator)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var yearPlanner = await _context.GetYearData(request.TeacherId, request.CalendarYear, cancellationToken);
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

            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
