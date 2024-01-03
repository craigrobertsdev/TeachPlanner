using FluentValidation;
using Mapster;
using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Authentication;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.Authentication;
using TeachPlanner.Shared.Contracts.Teachers;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Users;
using TeachPlanner.Api.Services.Authentication;

namespace TeachPlanner.Api.Features.Authentication;

public static class Register {
    public static async Task<IResult> Delegate(RegisterRequest request, ISender sender,
        CancellationToken cancellationToken) {
        var command = request.Adapt<Command>();
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    public record Command(string FirstName, string LastName, string Email, string Password, string ConfirmedPassword)
        : IRequest<AuthenticationResponse>;


    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(14);
        }
    }

    internal sealed class Handler : IRequestHandler<Command, AuthenticationResponse> {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public Handler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            ITeacherRepository teacherRepository,
            IUnitOfWork unitOfWork) {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken) {
            if (request.Password != request.ConfirmedPassword) throw new PasswordsDoNotMatchException();

            var user = await _userRepository.GetByEmail(request.Email, cancellationToken);
            if (user is not null) throw new DuplicateEmailException();

            user = new User(request.Email, PasswordService.HashPassword(request.Password));
            _userRepository.Add(user);

            var teacher = Teacher.Create(user.Id.Value.ToString(), request.FirstName, request.LastName);
            _teacherRepository.Add(teacher);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var token = _jwtTokenGenerator.GenerateToken(teacher);
            //var response = new TeacherResponse(
            //    teacher.Id.Value,
            //    teacher.FirstName,
            //    teacher.LastName);

            var response = default(TeacherResponse);

            return new AuthenticationResponse(response, token);
        }
    }
}