using Carter;
using Mapster;
using MediatR;
using TeachPlanner.Api.Contracts.Authentication;

namespace TeachPlanner.Api.Features.Authentication;

public class AuthenticationModule : CarterModule
{
    public AuthenticationModule() : base("/api")
    { }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginRequest request, ISender sender) =>
        {
            var command = request.Adapt<Login.Command>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        });

        app.MapPost("/register", async (RegisterRequest request, ISender sender) =>
        {
            var command = request.Adapt<Register.Command>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        });
    }
}
