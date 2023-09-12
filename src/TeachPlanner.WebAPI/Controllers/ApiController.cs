using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeachPlanner.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
}
