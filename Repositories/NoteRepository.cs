using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsoleProject.NET.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public NoteRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NoteVM> GetByIdAsync(int id)
    {
        var note = await _context.Notes
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == id);

        return note == null 
            ? throw new NoteNotFoundException(id) 
            : _mapper.Map<NoteVM>(note);
    }

    public async Task<IReadOnlyList<NoteVM>> GetByUserIdAsync(int userId)
    {
        var notes = await _context.Notes
            .Where(n => n.UserId == userId)
            .Include(n => n.User)
            .ToListAsync();

        return _mapper.Map<IReadOnlyList<NoteVM>>(notes);
    }

    public async Task<int> AddAsync(NoteAddDto dto)
    {
        var note = _mapper.Map<Note>(dto);
        note.NoteCreationTime = DateTime.UtcNow;
        
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
        
        return note.Id;
    }

    public async Task UpdateAsync(int id, NoteUpdateDto dto)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) throw new NoteNotFoundException(id);

        _mapper.Map(dto, note);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) throw new NoteNotFoundException(id);

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleCompletionAsync(int id, bool isCompleted)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) throw new NoteNotFoundException(id);

        note.IsCompleted = isCompleted;
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<NoteVM>> FilterByPriorityAsync(Priority priority)
    {
        var notes = await _context.Notes
            .Where(n => n.Priority == priority)
            .Include(n => n.User)
            .ToListAsync();

        return _mapper.Map<IReadOnlyList<NoteVM>>(notes);
    }
}