using FluentValidation;
using Mapster;
using MediatR;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Authentication;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Contracts.Authentication;
using TeachPlanner.Shared.Contracts.Teachers;
using TeachPlanner.Api.Services.Authentication;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TeachPlanner.Api.Features.Authentication;

//public static class Login {
//    public static async Task<IResult> Delegate(LoginModel request, ISender sender,
//        CancellationToken cancellationToken) {
//        var command = request.Adapt<Command>();
//        var result = await sender.Send(command, cancellationToken);
//        return Results.Ok(result);
//    }

//    private static string NormaliseEmail(string email) {
//        return email.Trim().ToUpper();
//    }

//    public record Command(string Email, string Password) : IRequest<AuthenticationResponse>;

//    public class Validator : AbstractValidator<Command> {
//        public Validator() {
//            RuleFor(x => x.Email).NotEmpty().EmailAddress();
//            RuleFor(x => x.Password).NotEmpty();
//        }
//    }

//    internal sealed class Handler : IRequestHandler<Command, AuthenticationResponse> {
//        private readonly IJwtTokenGenerator _jwtTokenGenerator;
//        private readonly ITeacherRepository _teacherRepository;
//        private readonly IUserRepository _userRepository;

//        public Handler(
//            IJwtTokenGenerator jwtTokenGenerator,
//            IUserRepository userRepository,
//            ITeacherRepository teacherRepository) {
//            _jwtTokenGenerator = jwtTokenGenerator;
//            _userRepository = userRepository;
//            _teacherRepository = teacherRepository;
//        }

//        public async Task<AuthenticationResponse> Handle(Command request, CancellationToken cancellationToken) {
//            var user = await _userRepository.GetByEmail(NormaliseEmail(request.Email), cancellationToken);
//            if (user == null || !PasswordService.VerifyPassword(request.Password, user.Password))
//                throw new InvalidCredentialsException();

//            var teacher = await _teacherRepository.GetByUserId(user.Id.Value.ToString(), cancellationToken);
//            if (teacher == null) throw new TeacherNotFoundException();

//            //var response = new TeacherModel(
//            //    teacher.Id.Value,
//            //    teacher.FirstName,
//            //    teacher.LastName);

//            var response = default(TeacherModel);

//            return new AuthenticationResponse(response, _jwtTokenGenerator.GenerateToken(teacher));
//        }
//    }
//}