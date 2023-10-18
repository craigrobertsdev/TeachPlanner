using FluentValidation;
using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Curriculum;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.YearDataRecords;

public static class SetSubjectsTaught
{
    public static async Task<IResult> Delegate(Guid teacherId, ISender sender, CancellationToken cancellationToken)
    {
        var command = new Command(
            new TeacherId(teacherId),
            new List<SubjectId>(),
            DateTime.Now.Year);

        var validationResult = new Validator().Validate(command);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        await sender.Send(command, cancellationToken);

        return Results.Ok();
    }

    public record Command(
        TeacherId TeacherId,
        List<SubjectId> SubjectIds,
        int CalendarYear
    ) : IRequest;

    private class Validator : AbstractValidator<Command>
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
        private readonly ICurriculumService _curriculumService;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IYearDataRepository _yearDataRepository;

        public Handler(IYearDataRepository yearDataRepository, ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork, ICurriculumService curriculumService)
        {
            _yearDataRepository = yearDataRepository;
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
            _curriculumService = curriculumService;
        }

        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var yearData =
                await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.CalendarYear,
                    cancellationToken);
            if (yearData == null) throw new YearDataNotFoundException();

            var subjects = _curriculumService.CurriculumSubjects
                .Where(subject => request.SubjectIds.Contains(subject.Id)).ToList();

            if (subjects.Count != request.SubjectIds.Count) throw new InvalidCurriculumSubjectIdException();

            yearData.AddSubjects(subjects);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}