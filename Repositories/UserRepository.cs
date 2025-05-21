using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using AutoMapper;
using ConsoleProject.NET.Contract;


namespace ConsoleProject.NET.Repositories;

public class UserRepository(IMapper mapper) : IUserRepository
{
    private readonly List<User> _users = new();
    private int _idCounter;
    private readonly IMapper _mapper = mapper;
    public UserVm? GetById(int id) =>
        _mapper.Map<UserVm>(_users.FirstOrDefault(x => x.Id == id));
    public IReadOnlyList<UserVm> GetUsers() => _mapper.Map<IReadOnlyList<UserVm>>(_users);
    public int Add(UserAddDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new NameIsRequired();
        var user = _mapper.Map<User>(dto);
        user.Id = ++_idCounter;
        _users.Add(user);
        return user.Id;
    }
}