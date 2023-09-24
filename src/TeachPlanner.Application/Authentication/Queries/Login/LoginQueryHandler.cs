using TeachPlanner.Application.Common.Exceptions;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Authentication.Common;
using TeachPlanner.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using FluentValidation;

namespace TeachPlanner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, UserManager<IdentityUser> userManager, ITeacherRepository teacherRepository, SignInManager<IdentityUser> signInManager)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _teacherRepository = teacherRepository;
        _signInManager = signInManager;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(request.Email));
        if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
        {
            throw new InvalidCredentialsException();
        }

        var teacher = await _teacherRepository.GetTeacherByEmailAsync(request.Email, cancellationToken);
        if (teacher == null)
        {
            throw new TeacherNotFoundException();
        }

        return new AuthenticationResult(teacher, _jwtTokenGenerator.GenerateToken(teacher));
    }
}
