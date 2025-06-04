using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IJwtTokenGenerator tokenGenerator, AppDbContext db) : ControllerBase
{
    [HttpPost("signup")]
    [AllowAnonymous]
    public IActionResult SignUp(SignUpDto dto)
    {
        var userExists = db.Users.Any(u => u.UserName == dto.UserName);
        if (userExists)
            return Conflict("User already exists");

        var user = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        db.Users.Add(user);
        db.SaveChanges();

        var token = tokenGenerator.Generate(user);
        db.SaveChanges();
        return Ok(new AuthResponse(token));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LogInDto dto)
    {
        var user = db.Users.FirstOrDefault(u =>
            u.UserName == dto.UserName && u.Password == dto.Password);

        if (user is null)
            return Unauthorized();

        var token = tokenGenerator.Generate(user);
        db.SaveChanges(); 
        return Ok(new AuthResponse(token));
    }
}
