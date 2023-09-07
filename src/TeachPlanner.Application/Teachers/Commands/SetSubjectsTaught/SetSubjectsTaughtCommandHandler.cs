using ErrorOr;
using MediatR;
using TeachPlanner.Application.Common.Errors;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandHandler : IRequestHandler<SetSubjectsTaughtCommand, ErrorOr<SetSubjectsTaughtResult>>
{
    private readonly ITeacherRepository _teacherRepository;

    public SetSubjectsTaughtCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<ErrorOr<SetSubjectsTaughtResult>> Handle(SetSubjectsTaughtCommand request, CancellationToken cancellationToken)
    {
        //var getSubjects = await _teacherRepository.GetSubjectsTaughtByTeacher(request.TeacherId, elaborations: null);
        try
        {
            var subjects = await _teacherRepository.SetSubjectsTaughtByTeacher(request.TeacherId, request.SubjectNames);

            if (subjects == null)
            {
                return Errors.Teacher.TeacherNotFound;
            }

            return new SetSubjectsTaughtResult(subjects);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Errors.Teacher.TeacherHasNoSubjects;
        }

    }
}
