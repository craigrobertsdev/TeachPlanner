using Application.Common.Interfaces.Persistence;
using Application.Teachers.Common;
using Application.Common.Errors;
using Domain.TeacherAggregate;
using ErrorOr;
using MediatR;

namespace Application.Teachers.CreateTeacher.Commands;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, ErrorOr<TeacherCreatedResult>>
{
    private readonly ITeacherRepository _teacherRepository;

    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<ErrorOr<TeacherCreatedResult>> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        if (await _teacherRepository.GetTeacherByIdAsync(command.TeacherId) is not null)
        {
            return Errors.Authentication.DuplicateId;
        }

        var teacher = Teacher.Create(command.FirstName, command.LastName, command.Email, command.Password);

        _teacherRepository.Create(teacher);

        return new TeacherCreatedResult(teacher.Id);
    }

}
