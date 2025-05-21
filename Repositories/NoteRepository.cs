using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class NoteRepository: INoteRepository
{
    private readonly AppDbContext
    private readonly List<Note> _notes = new();
    private int _idCounter;
    private readonly IMapper _mapper = mapper;
    public NoteVM? GetById(int id)
    => _mapper.Map<NoteVM>(_notes.FirstOrDefault(z => z.Id == id));
    public IReadOnlyList<NoteVM> GetByUserId(int userId)
    {
        if (userRepository.GetById(userId) == null)
            throw new UserNotFoundException(userId);
        return _mapper.Map<IReadOnlyList<NoteVM>>(_notes.Where(x => x.UserId == userId));
    }
    public int Add(NoteAddDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new TitleIsRequired();
        var note = _mapper.Map<Note>(dto);
        note.Id = _idCounter++;
        _notes.Add(note);
        return note.Id;
    }
    public void Update(int id, NoteUpdateDto dto)
    {
        var note = _notes.FirstOrDefault(o => o.Id == id)
        ??    throw new NoteNotFoundException(id);
        _mapper.Map(dto, note);
    }
    public void Delete(int id)
    {
        var note = _notes.FirstOrDefault(v => v.Id == id)
        ??    throw new NoteNotFoundException(id);
        _notes.Remove(note);
    }
}
