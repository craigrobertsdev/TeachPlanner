using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandHandler : IRequestHandler<SetSubjectsTaughtCommand>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetSubjectsTaughtCommandHandler(ITeacherRepository teacherRepository, ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _subjectRepository = subjectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SetSubjectsTaughtCommand command, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetById(command.TeacherId, cancellationToken);

        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }

        CheckNewSubjectsToBeSaved(teacher.GetYearData(command.CalendarYear), command);

        var subjects = await _subjectRepository.GetSubjectsById(command.SubjectIds, cancellationToken);

        teacher.AddSubjectsTaught(subjects, command.CalendarYear);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void CheckNewSubjectsToBeSaved(YearData? yearData, SetSubjectsTaughtCommand command)
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
