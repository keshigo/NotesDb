using ConsoleProject.NET.Config;
using ConsoleProject.NET.Configurations.Database;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Repositories.Interfaces;
using ConsoleProject.NET.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ConsoleProject.NET;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddOptions<ApplicationDbContextSettings>()
            .Bind(config.GetRequiredSection(nameof(ApplicationDbContextSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        services.AddAuthorization();

        services.AddOptions<JwtOptions>()
            .Bind(config.GetRequiredSection(nameof(JwtOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
            });
        });

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(typeof(Composer).Assembly);
        services.AddExceptionHandler<ExceptionHandler>();

        return services;
    }

}
