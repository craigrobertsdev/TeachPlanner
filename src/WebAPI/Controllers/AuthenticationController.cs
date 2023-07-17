using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Authentication;
using MapsterMapper;
using WebAPI.Authentication;
using Application.Authentication.Commands.Register;
using Application.Common;
using Application.Authentication.Common;
using Application.Common.Exceptions;
using WebAPI.Contracts.Authentication;
using Application.Authentication.Queries.Login;

namespace WebAPI.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        Either<AuthenticationResult, AuthenticationException> result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            authException => BadRequest(authException.Message));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        Either<AuthenticationResult, AuthenticationException> result = await _mediator.Send(command);

        return result.Match<IActionResult>(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            authException => BadRequest(authException.Message));
    }
}
