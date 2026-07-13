using System.Net;
using System.Text.Json;
using GymMangV2.api.Models;
using GymMangV2.Application.Exceptions;
namespace GymMangV2.api.Middelware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next; // This pass the request next middleware
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
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
            await HandleException(context, ex);
        }
    }

    public static async Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        int statusCode;
        string message;

        switch (ex)
        {
            case BusinessException:
                statusCode = StatusCodes.Status400BadRequest;
                message = ex.Message;
                break;
            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = ex.Message;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;

            //can add multiple exception
        }

        context.Response.StatusCode = statusCode;
        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = message,

            TimeStamp = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);

    }

}