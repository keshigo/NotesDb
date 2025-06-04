using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.Config;

public class JwtOptions
{
    [Required]
    public required string Issuer { get; init; }

    [Required]
    public required string Audience { get; init; }

    [Required]
    public required string Secret { get; init; }
}
