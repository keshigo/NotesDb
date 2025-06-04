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

public class User
{
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}