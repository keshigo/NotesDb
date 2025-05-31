using ConsoleProject.NET.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ConsoleProject.NET.Services;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        context.Response.ContentType = "application/json";
        
        switch (exception)
        {
            case NoteNotFoundException ex:
                await HandleException(context, ex, HttpStatusCode.NotFound);
                break;
                
            case UserNotFoundException ex:
                await HandleException(context, ex, HttpStatusCode.NotFound);
                break;
                
            default:
                await HandleException(context, exception, HttpStatusCode.InternalServerError);
                break;
        }
        
        return true;
    }

    private static async Task HandleException(
        HttpContext context, 
        Exception exception, 
        HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsJsonAsync(new {
            Error = exception.Message,
            StatusCode = code
        });
    }
}