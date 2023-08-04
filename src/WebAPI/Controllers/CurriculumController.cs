using Infrastructure.Curriculum;
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

    [HttpGet("parseCurriculum")]
    public IActionResult ParseCurriculum()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2V1hhQlJAfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5bdkFiX3xac3ZXRWdZ");

        var curriculumParser = new CurriculumParser();

        var subjects = curriculumParser.GetCurriculumData();

        return Ok(subjects);
    }
}
