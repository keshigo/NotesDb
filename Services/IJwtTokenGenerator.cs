using ConsoleProject.NET.Models;

public interface IJwtTokenGenerator
{
    JwtToken Generate(User user);
}
