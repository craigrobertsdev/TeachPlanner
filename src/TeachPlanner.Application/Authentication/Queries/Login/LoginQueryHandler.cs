using TeachPlanner.Application.Common.Exceptions;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Authentication.Common;
using TeachPlanner.Application.Common.Interfaces.Authentication;

namespace TeachPlanner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, ITeacherRepository teacherRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _teacherRepository = teacherRepository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByEmailAsync(request.Email, cancellationToken);

        if (teacher == null)
        {
            throw new InvalidCredentialsException();
        }

        // TODO - Hash password
        if (teacher.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }

        var token = _jwtTokenGenerator.GenerateToken(teacher);

        return new AuthenticationResult(teacher, token);

    }
}
