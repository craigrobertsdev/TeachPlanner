using MediatR;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandHandler : IRequestHandler<SetSubjectsTaughtCommand, SetSubjectsTaughtResult>
{
    private readonly ITeacherRepository _teacherRepository;

    public SetSubjectsTaughtCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<SetSubjectsTaughtResult> Handle(SetSubjectsTaughtCommand request, CancellationToken cancellationToken)
    {
        var subjects = await _teacherRepository.SetSubjectsTaughtByTeacher(request.TeacherId, request.SubjectNames, cancellationToken);

        if (subjects == null)
        {
            throw new TeacherHasNoSubjectsException();
        }

        return new SetSubjectsTaughtResult(subjects);
    }
}
