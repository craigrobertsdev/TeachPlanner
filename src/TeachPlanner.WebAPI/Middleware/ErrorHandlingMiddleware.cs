using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeachPlanner.Api.Common.Errors;
using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex is BaseException bex ? bex.StatusCode : (int)HttpStatusCode.InternalServerError;

        var problemDetails = new ProblemDetails()
        {
            Status = context.Response.StatusCode,
            Title = ex.Message,
            Detail = ex.Message + " --- Make sure to change this in production!",
            // Detail = context.Response.StatusCode == (int)HttpStatusCode.InternalServerError ? "Internal Server Error" : ex.Message
        };

        await context.Response.WriteAsync(problemDetails.ToString() != null
            ? (problemDetails.ToString())!
            : "An internal server error has occurred");
    }
}
