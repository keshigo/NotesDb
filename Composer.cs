using System.Reflection;
using ConsoleProject.NET;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace SimpleExample;

public static class Composer
{
    public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration config)
    {
        services
        .AddSingleton<IUserRepository, UserRepository>()
        .AddSingleton<INoteRepository, NoteRepository>()
        .AddAutoMapper(typeof(UserProfile), typeof(NoteProfile))
        .AddExceptionHandler<ExceptionHandler>();
        return services;
    }
    public class Exceptions : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                NoteNotFoundException => 404,
                UserNotFoundException => 404,
                TitleIsRequired => 400,
                NameIsRequired => 400,
                _ => 500
            };
            await context.Response.WriteAsJsonAsync(new { Error = exception.Message });
            return true;
        }
    }

    
    
}