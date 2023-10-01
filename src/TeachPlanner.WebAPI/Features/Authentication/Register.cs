using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Authentication;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Authentication;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.Authentication;

public static class Register
{
    public record Command(string FirstName, string LastName, string Email, string Password, string ConfirmedPassword)
        : IRequest<AuthenticationResponse>;


    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(14);
        }
    }

    internal sealed class Handler : IRequestHandler<Command, AuthenticationResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            ApplicationDbContext context,
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmedPassword)
            {
                throw new PasswordsDoNotMatchException();
            }

            if (_userManager.FindByEmailAsync(request.Email).Result != null)
            {
                throw new DuplicateEmailException();
            }

            var identityUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded == false)
            {
                throw new UserRegistrationFailedException();
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var teacher = Teacher.Create(Guid.Parse(user!.Id), request.FirstName, request.LastName);

            _context.Teachers.Add(teacher);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(teacher);
            return new AuthenticationResponse(teacher, token);
        }
    }

    public static async Task<IResult> Delegate(RegisterRequest request, ISender sender)
    {
        var command = request.Adapt<Command>();
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}

