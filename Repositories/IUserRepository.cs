using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public interface IUserRepository
{
    Task<UserVm> GetById(int id);
    Task<IReadOnlyList<UserVm>> GetUsers();
    Task<int> Add(UserAddDto dto);
    Task<User?> GetUserWithNotesAsync(int userId);
}