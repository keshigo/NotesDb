using ConsoleProject.NET.Contract;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleProject.NET.Repositories.Interfaces;

public interface INoteRepository
{
    Task<NoteVM> GetByIdAsync(int id);
    Task<IReadOnlyList<NoteVM>> GetByUserIdAsync(int userId);
    Task<int> AddAsync(NoteAddDto dto);
    Task UpdateAsync(int id, NoteUpdateDto dto);
    Task DeleteAsync(int id);
    Task ToggleCompletionAsync(int id, bool isCompleted);
}