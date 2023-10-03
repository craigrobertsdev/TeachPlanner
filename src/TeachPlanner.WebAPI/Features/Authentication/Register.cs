using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Authentication;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Authentication;
using TeachPlanner.Api.Contracts.Teachers;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Services.Authentication;

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
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            ApplicationDbContext context,
            IUnitOfWork unitOfWork)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmedPassword)
            {
                throw new PasswordsDoNotMatchException();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user is not null)
            {
                throw new DuplicateEmailException();
            }

            user = new User(request.Email, PasswordService.HashPassword(request.Password));
            _context.Users.Add(user);

            var teacher = Teacher.Create(user.Id, request.FirstName, request.LastName);
            _context.Teachers.Add(teacher);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(teacher);
            var response = new TeacherResponse(
                teacher.Id.Value,
                teacher.FirstName,
                teacher.LastName,
                teacher.Resources.Select(r => r.Id.Value).ToList());

            return new AuthenticationResponse(response, token);
        }
    }

    public static async Task<IResult> Delegate(RegisterRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = request.Adapt<Command>();
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}

