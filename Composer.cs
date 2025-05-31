using ConsoleProject.NET.Data;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Repositories.Interfaces;
using ConsoleProject.NET.Services;
using Microsoft.EntityFrameworkCore;

namespace ConsoleProject.NET;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Default")));
        
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddExceptionHandler<ExceptionHandler>();
        
        return services;
    }
}