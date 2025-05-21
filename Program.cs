using Microsoft.AspNetCore.Builder;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using SimpleExample;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
