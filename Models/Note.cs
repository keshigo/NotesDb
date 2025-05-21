using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.NET.Models;

public class Note
{
    public int Id { get; set; }
    //[MaxLength(20)]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime NoteCreationTime { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
public enum Priority
{
    low,
    mid,
    high
}