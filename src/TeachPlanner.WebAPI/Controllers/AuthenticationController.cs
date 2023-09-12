using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using TeachPlanner.Application.Common.Exceptions;
using FluentValidation;
using TeachPlanner.Application.Authentication.Queries.Login;
using TeachPlanner.Contracts.Authentication;
using TeachPlanner.Application.Authentication.Commands.Register;

namespace TeachPlanner.Api.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;
    private readonly IValidator<RegisterCommand> _registerValidator;
    private readonly IValidator<LoginQuery> _loginValidator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMapper mapper, ISender sender,
        IValidator<RegisterCommand> registerValidator, IValidator<LoginQuery> loginValidator)
    {
        _mapper = mapper;
        _sender = sender;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var validationResult = await _registerValidator.ValidateAsync(command);

        var authResult = await _sender.Send(command);

        return Ok(authResult);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var validationResult = await _loginValidator.ValidateAsync(query);

        var authenticationResult = await _sender.Send(query);

        return Ok(authenticationResult);
    }
}
