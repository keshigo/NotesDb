using ConsoleProject.NET.Configurations.Database;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Repositories.Interfaces;
using ConsoleProject.NET.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddOptions<ApplicationDbContextSettings>()
    .Bind(builder.Configuration.GetRequiredSection(nameof(ApplicationDbContextSettings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();


builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<ExceptionHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler("/error");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();
