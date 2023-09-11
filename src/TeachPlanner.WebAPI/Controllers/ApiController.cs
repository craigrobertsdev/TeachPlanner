using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeachPlanner.Api.Common.Http;

namespace TeachPlanner.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
}
