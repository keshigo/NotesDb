using ConsoleProject.NET.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_NoteThirdTask.Services
{
    public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        context.Response.ContentType = "application/json";
        
        switch (exception)
        {
            case DbUpdateException:
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Database error");
                break;
                
            case NoteNotFoundException ex:
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Error = ex.Message });
                break;
                
            default:
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal server error");
                break;
        }
        
        return true;
    }
}
}