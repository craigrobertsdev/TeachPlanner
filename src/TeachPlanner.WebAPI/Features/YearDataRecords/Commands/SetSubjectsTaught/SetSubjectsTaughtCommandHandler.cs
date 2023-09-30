using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.YearDataRecords;
using TeachPlanner.Api.Features.Common.Exceptions;

namespace TeachPlanner.Api.Features.YearDataRecords.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandHandler : IRequestHandler<SetSubjectsTaughtCommand>
{
    private readonly IYearDataRepository _yearDataRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetSubjectsTaughtCommandHandler(IYearDataRepository yearDataRepository, ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
    {
        _yearDataRepository = yearDataRepository;
        _subjectRepository = subjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SetSubjectsTaughtCommand command, CancellationToken cancellationToken)
    {
        var yearData = await _yearDataRepository.GetByTeacherAndYear(command.TeacherId, command.CalendarYear, cancellationToken);

        if (yearData == null)
        {
            yearData = YearData.Create(command.CalendarYear);
        }

        CheckNewSubjectsToBeSaved(yearData, command.SubjectIds);
        var subjects = await _subjectRepository.GetSubjectsById(command.SubjectIds, cancellationToken);

        yearData.AddSubjects(subjects);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void CheckNewSubjectsToBeSaved(YearData yearData, List<SubjectId> subjectIds)
    {
        if (yearData.Subjects.Count != subjectIds.Count)
        {
            return;
        }

        if (yearData is null)
        {
            return;
        }

        if (subjectIds.Count == 0)
        {
            throw new NoNewSubjectsTaughtException();
        }

        foreach (var subjectId in subjectIds)
        {
            if (!yearData.Subjects.Any(s => s.Id == subjectId))
            {
                return;
            }
        }

        throw new NoNewSubjectsTaughtException();
    }
}
