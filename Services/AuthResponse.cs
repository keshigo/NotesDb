using ConsoleProject.NET.Models;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }

    public AuthResponse(JwtToken token)
    {
        Token = token.Token;
        ExpiresAt = token.ExpiresAt;
    }
}
