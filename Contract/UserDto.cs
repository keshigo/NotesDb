using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.Contract;

public record UserAddDto(
    [Required(ErrorMessage = "Name is required")] string Name,
    [Required(ErrorMessage = "Password is required")] string Password
);
public record UserVm(int id, string Name);