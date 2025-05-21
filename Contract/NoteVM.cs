using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Contract;

public record NoteVM(int Id, string Title, string Description, DateTime NoteCreationTime, Priority priority, bool IsCompleted);