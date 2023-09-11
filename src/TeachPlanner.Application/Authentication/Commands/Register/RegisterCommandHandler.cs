using TeachPlanner.Application.Common.Errors;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Authentication.Common;
using TeachPlanner.Application.Common.Interfaces.Authentication;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
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

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _teacherRepository.GetTeacherByEmailAsync(command.Email) != null)
        {
            throw new DuplicateEmailException();
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
