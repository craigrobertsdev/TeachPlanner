using System.Net;
using Microsoft.AspNetCore.Mvc;
using TeachPlanner.Api.Common.Exceptions;

namespace TeachPlanner.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
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
        string type;
        if (ex is BaseException baseException)
        {
            context.Response.StatusCode = baseException.StatusCode;
            type = baseException.Type ?? "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        }

        var problemDetails = new ProblemDetails
        {
            Type = type,
            Title = ex.Message,
            Status = context.Response.StatusCode,
            Detail = ex.Message + " --- Make sure to change this in production!"
            // Detail = context.Response.StatusCode == (int)HttpStatusCode.InternalServerError ? "Internal Server Error" : ex.Message
        };

        await context.Response.WriteAsync(problemDetails.Detail != null
            ? problemDetails.Detail!
            : "An internal server error has occurred");
    }
}