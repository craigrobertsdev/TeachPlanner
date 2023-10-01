using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Authentication;
using TeachPlanner.Api.Contracts.Authentication;
using TeachPlanner.Api.Database;

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
        private readonly ApplicationDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            SignInManager<IdentityUser> signInManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(_userManager.NormalizeEmail(request.Email));
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new InvalidCredentialsException();
            }

            var userId = Guid.Parse(user.Id);

            var teacher = await _context.Teachers
                .Where(t => t.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            return new AuthenticationResponse(teacher, _jwtTokenGenerator.GenerateToken(teacher));
        }
    }
    public static async Task<IResult> Delegate(LoginRequest request, ISender sender)
    {
        var command = request.Adapt<Login.Command>();
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}
