using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.Models;

public class Note
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public DateTime NoteCreationTime { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public int UserId { get; set; }
    public required virtual User User { get; set; } = null!;
}

public enum Priority
{
    low,
    mid,
    high
}
