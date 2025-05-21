using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public interface INoteRepository
{
    Task<NoteVM> GetById(int id);
    Task<IReadOnlyList<NoteVM>> GetByUserId(int userId);
    Task<int> Add(NoteAddDto dto);
    Task Delete(int id);
    Task Update(int id, NoteUpdateDto dto);
}