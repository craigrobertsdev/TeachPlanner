using Application.Authentication.Common;
using Application.Common;
using Application.Common.Exceptions.Authentication;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;
using MediatR;

namespace Application.Authentication.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Either<AuthenticationResult, DuplicateUserException>>
{
    private IJwtTokenGenerator _jwtTokenGenerator;
    private IUserRepository _userRepository;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository
    )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

#pragma warning disable CS1998
    public async Task<Either<AuthenticationResult, DuplicateUserException>> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken
    )
    {
        if (_userRepository.GetUserByEmail(command.Email) != null)
        {
            return new Either<AuthenticationResult, DuplicateUserException>(
                new DuplicateUserException()
            );
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new Either<AuthenticationResult, DuplicateUserException>(
            new AuthenticationResult(user, token)
        );
    }
}
