using TeachPlanner.Application.Common.Exceptions;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Authentication.Common;
using TeachPlanner.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ITeacherRepository _teacherRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        UserManager<IdentityUser> userManager,
        ITeacherRepository teacherRepository,
        IUnitOfWork unitOfWork
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (command.Password != command.ConfirmedPassword)
        {
            throw new PasswordsDoNotMatchException();
        }

        if (_userManager.FindByEmailAsync(command.Email).Result != null)
        {
            throw new DuplicateEmailException();
        }

        var identityUser = new IdentityUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(identityUser, command.Password);
        if (result.Succeeded == false)
        {
            throw new UserRegistrationFailedException();
        }

        var teacher = Teacher.Create(Guid.NewGuid(), command.FirstName, command.LastName);
        var user = await _userManager.FindByEmailAsync(command.Email);
        teacher.AddUserId(Guid.Parse(user!.Id));

        _teacherRepository.Create(teacher);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(teacher);
        return new AuthenticationResult(teacher, token);
    }
}
