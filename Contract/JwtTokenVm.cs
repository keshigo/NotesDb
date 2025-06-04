public record JwtTokenVm
{
    public int UserId { get; init; }
    public string Token { get; init; }
    public DateTime ExpiresAt { get; init; }

    public JwtTokenVm(int userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }
}
