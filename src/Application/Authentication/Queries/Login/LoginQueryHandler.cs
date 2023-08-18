using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Errors;
using ErrorOr;
using MediatR;
using Domain.TeacherAggregate;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, ITeacherRepository teacherRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _teacherRepository = teacherRepository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByEmail(request.Email);

        if (teacher == null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        // TODO - Hash password
        if (teacher.Password != request.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(teacher);

        return new AuthenticationResult(teacher, token);

    }
}
