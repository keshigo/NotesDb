using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Utils;

namespace ConsoleProject.NET.Services;

public interface IAuthService
{
    JwtTokenVm SignUp(SignUpDto dto);
    JwtTokenVm? LogIn(LogInDto dto);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator)
    {
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public JwtTokenVm SignUp(SignUpDto dto)
    {
        var user = new User
        {

            UserName = dto.UserName,
            Password = PasswordHasher.HashPassword(dto.Password)
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        var token = UpdateToken(user);
        _dbContext.SaveChanges();

        return new JwtTokenVm(user.Id, token.Token, token.ExpiresAt);
    }

    public JwtTokenVm? LogIn(LogInDto dto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.UserName == dto.UserName);
        if (user is null || !PasswordHasher.VerifyPassword(user.Password, dto.Password))
            return null;

        var token = UpdateToken(user);
        _dbContext.SaveChanges();

        return new JwtTokenVm(user.Id, token.Token, token.ExpiresAt);
    }

    private JwtToken UpdateToken(User user)
    {
        var oldToken = _dbContext.JwtTokens.FirstOrDefault(t => t.UserId == user.Id);
        if (oldToken is not null)
            _dbContext.JwtTokens.Remove(oldToken);

        var newToken = _jwtTokenGenerator.Generate(user);
        _dbContext.JwtTokens.Add(newToken);

        return newToken;
    }
}
