using FluentValidation;
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
        private readonly IYearDataRepository _yearDataRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Command> _validator;

        public Handler(IYearDataRepository yearDataRepository, ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, IValidator<Command> validator)
        {
            _yearDataRepository = yearDataRepository;
            _subjectRepository = subjectRepository;
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

            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.CalendarYear, cancellationToken);
            if (yearData == null)
            {
                throw new YearDataNotFoundException();
            }

            var subjects = await _subjectRepository.GetSubjectsById(request.SubjectIds, cancellationToken);

            if (subjects.Count != request.SubjectIds.Count)
            {
                throw new SubjectNotFoundException();
            }

            yearData.AddSubjects(subjects);

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
