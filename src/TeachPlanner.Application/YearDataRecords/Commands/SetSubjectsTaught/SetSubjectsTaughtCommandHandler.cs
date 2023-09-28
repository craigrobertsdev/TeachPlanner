using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.YearDataRecords.Commands.SetSubjectsTaught;
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

        var subjects = await _subjectRepository.GetSubjectsById(command.SubjectIds, cancellationToken);

        yearData.AddSubjects(subjects);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void CheckNewSubjectsToBeSaved(YearData yearData, SetSubjectsTaughtCommand command)
    {
        if (yearData is null)
        {
            return;
        }

        if (command.SubjectIds.Count == 0)
        {
            throw new NoNewSubjectsTaughtException();
        }

        if (yearData.Subjects.Count != command.SubjectIds.Count)
        {
            return;
        }

        foreach (var subjectId in command.SubjectIds)
        {
            if (!yearData.Subjects.Any(s => s.Id == subjectId))
            {
                return;
            }
        }

        throw new NoNewSubjectsTaughtException();
    }
}
