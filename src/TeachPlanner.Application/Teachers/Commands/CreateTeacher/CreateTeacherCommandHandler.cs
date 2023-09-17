using TeachPlanner.Application.Common.Exceptions;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Teachers.Common;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, TeacherCreatedResult>
{
    private readonly ITeacherRepository _teacherRepository;

    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<TeacherCreatedResult> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        if (await _teacherRepository.GetById(command.TeacherId, cancellationToken) is not null)
        {
            throw new DuplicateEmailException();
        }

        var teacher = Teacher.Create(command.FirstName, command.LastName, command.Email, command.Password);

        _teacherRepository.Create(teacher, cancellationToken);

        return new TeacherCreatedResult(teacher.Id);
    }

}
