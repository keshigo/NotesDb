using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Data;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleProject.NET.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserVm> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null
            ? throw new UserNotFoundException(id)
            : _mapper.Map<UserVm>(user);
    }

    public async Task<IReadOnlyList<UserVm>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IReadOnlyList<UserVm>>(users);
    }

    public async Task<int> Add(UserAddDto dto)
    {
        var user = _mapper.Map<User>(dto);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }
    public async Task<User?> GetUserWithNotesAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Notes)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}