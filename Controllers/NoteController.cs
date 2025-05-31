using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using System.Collections.Generic;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Repositories.Interfaces;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteRepository _noteRepository;
    
    public NoteController(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IReadOnlyList<NoteVM>>> GetByUser(int userId)
    {
        var notes = await _noteRepository.GetByUserIdAsync(userId);
        return Ok(notes);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] NoteAddDto dto)
    {
        var id = await _noteRepository.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteVM>> GetById(int id)
    {
        var note = await _noteRepository.GetByIdAsync(id);
        return note != null ? Ok(note) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] NoteUpdateDto dto)
    {
        await _noteRepository.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _noteRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleCompletion(int id, [FromBody] bool isCompleted)
    {
        await _noteRepository.ToggleCompletionAsync(id, isCompleted);
        return NoContent();
    }
}