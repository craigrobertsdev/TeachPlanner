using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class CurriculumController : ApiController
{
    [HttpGet]
    public IActionResult ListSubjects()
    {
        return Ok(Array.Empty<string>());
    }
}
