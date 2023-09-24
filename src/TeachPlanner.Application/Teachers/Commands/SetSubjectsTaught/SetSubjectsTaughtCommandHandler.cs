using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;

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

        var subjects = await _subjectRepository.GetSubjectsById(command.SubjectIds, cancellationToken);

        _teacherRepository.SetSubjectsTaughtByTeacher(teacher, subjects, command.CalendarYear);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
