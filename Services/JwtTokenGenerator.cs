using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConsoleProject.NET.Config;
using ConsoleProject.NET.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleProject.NET.Services;


public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;

    public JwtTokenGenerator(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public JwtToken Generate(User user)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Convert.FromBase64String(_options.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(5);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new JwtToken
        {
            Token = jwt,
            UserId = user.Id,
            CreatedAt = now,
            ExpiresAt = expires
        };
    }
}
