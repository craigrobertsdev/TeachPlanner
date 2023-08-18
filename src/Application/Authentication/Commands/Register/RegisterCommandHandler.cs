using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Errors;
using ErrorOr;
using MediatR;
using Domain.TeacherAggregate;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ITeacherRepository _teacherRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        ITeacherRepository teacherRepository
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _teacherRepository = teacherRepository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_teacherRepository.GetTeacherByEmail(command.Email) != null)
        {
            return Errors.Authentication.DuplicateEmail;
        }

        var teacher = Teacher.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password);

        _teacherRepository.Create(teacher);

        var token = _jwtTokenGenerator.GenerateToken(teacher);

        return new AuthenticationResult(teacher, token);
    }

}
