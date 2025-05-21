using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Contract;

public record NoteAddDto(string Title, string Description, Priority priority, int UserId);

public record NoteUpdateDto(string? Title, string? Description, Priority? priority, bool? IsCompleted);