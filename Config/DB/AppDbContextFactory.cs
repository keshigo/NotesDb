using ConsoleProject.NET.Configurations.Database;
using ConsoleProject.NET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleProject.NET;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false)
            .AddUserSecrets<AppDbContextFactory>()
            .Build();

        var services = new ServiceCollection();
        services
            .Configure<ApplicationDbContextSettings>(
                configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)));
        services.AddOptions<ApplicationDbContextSettings>()
            .Bind(configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<AppDbContext>();

        var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<AppDbContext>();
    }
}
