using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using System.Collections.Generic;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController(INoteRepository noteRepo) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public ActionResult<IReadOnlyList<NoteVM>> GetByUser(int userId)
    => Ok(noteRepo.GetByUserId(userId));
    
    [HttpPost]
    public ActionResult<int> Create([FromBody] NoteAddDto dto)
    {
        var id = noteRepo.Add(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }
    [HttpGet("{id}")]
    public ActionResult<NoteVM> GetById(int id)
    => noteRepo.GetById(id) is { } note ? Ok(note) : NotFound();
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] NoteUpdateDto dto)
    {
        noteRepo.Update(id, dto);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        noteRepo.Delete(id);
        return NoContent();
    }
}