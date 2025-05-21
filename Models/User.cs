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
    //[Required]
    //[MaxLength(20)]
    public string Name { get; set; }
    //[Required]
    public string Password { get; set; }
    public List<Note> Notes { get; set; } = new();
}