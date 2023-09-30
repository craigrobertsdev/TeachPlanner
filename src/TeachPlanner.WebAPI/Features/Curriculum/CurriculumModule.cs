using Carter;
using MediatR;

namespace TeachPlanner.Api.Features.Curriculum;

public class CurriculumModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/parsecurriculum", async (ISender sender) =>
        {

            var command = new ParseCurriculum.Command();
            await sender.Send(command);

            return Results.Ok();
        });
    }
}
