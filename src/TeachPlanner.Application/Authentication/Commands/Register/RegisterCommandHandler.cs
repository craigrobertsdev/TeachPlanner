using TeachPlanner.Application.Common.Exceptions;
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
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        ITeacherRepository teacherRepository,
        IUnitOfWork unitOfWork
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _teacherRepository.GetTeacherByEmailAsync(command.Email, cancellationToken) != null)
        {
            throw new DuplicateEmailException();
        }

        var teacher = Teacher.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password);

        _teacherRepository.Create(teacher);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(teacher);

        return new AuthenticationResult(teacher, token);
    }
}
