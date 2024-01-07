using FluentValidation;
using Mapster;
using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Authentication;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Api.Features.Authentication;

public static class Login {
    public static async Task<IResult> Delegate(LoginModel request, ISender sender,
           CancellationToken cancellationToken) {
        var command = request.Adapt<Command>();
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static string NormaliseEmail(string email) {
        return email.Trim().ToUpper();
    }

    public record Command(string Email, string Password) : IRequest<AuthenticationResponse>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    internal sealed class Handler : IRequestHandler<Command, AuthenticationResponse> {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ITeacherRepository _teacherRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            ITeacherRepository teacherRepository,
            UserManager<ApplicationUser> userManager) {
            _jwtTokenGenerator = jwtTokenGenerator;
            _teacherRepository = teacherRepository;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken) {
            var user = await _userManager.FindByEmailAsync(NormaliseEmail(request.Email));

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password)) {
                throw new InvalidCredentialsException();
            }

            var teacher = await _teacherRepository.GetByUserId(user.Id, cancellationToken);
            if (teacher == null) throw new TeacherNotFoundException();

            var tokenResponse = _jwtTokenGenerator.GenerateToken(teacher);

            return new AuthenticationResponse(tokenResponse.Token, tokenResponse.Expiration, string.Empty);
        }
    }
}