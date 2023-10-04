using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Authentication;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Authentication;
using TeachPlanner.Api.Contracts.Teachers;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Services.Authentication;

namespace TeachPlanner.Api.Features.Authentication;

public static class Login
{
    public record Command(string Email, string Password) : IRequest<AuthenticationResponse>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, AuthenticationResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(NormaliseEmail(request.Email), cancellationToken);
            if (user == null || !PasswordService.VerifyPassword(request.Password, user.Password))
            {
                throw new InvalidCredentialsException();
            }

            var teacher = await _context.GetTeacherByUserId(user.Id);
            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            var response = new TeacherResponse(
                teacher.Id.Value,
                teacher.FirstName,
                teacher.LastName,
                teacher.Resources.Select(r => r.Id.Value).ToList());

            return new AuthenticationResponse(response, _jwtTokenGenerator.GenerateToken(teacher));
        }
    }
    public static async Task<IResult> Delegate(LoginRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = request.Adapt<Command>();
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
    private static string NormaliseEmail(string email)
    {
        return email.Trim().ToUpper();
    }
}

