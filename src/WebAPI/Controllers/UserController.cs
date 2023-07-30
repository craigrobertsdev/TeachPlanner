using Application.Users.Queries.GetAllUsers;
using Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[AllowAnonymous]
[Route("[controller]")]
public class UserController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UserController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Mapping doesn't work here. Created to confirm that users are being retrieved from the database.
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());

        return result.Match(
            users => Ok(_mapper.Map<GetAllUsersResponse>(users)),
            errors => Problem(errors));


    }
}
