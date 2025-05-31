using ConsoleProject.NET.Contract;

using ConsoleProject.NET.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserVm>>> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] UserAddDto dto)
    {
        var id = await _userRepository.Add(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserVm>> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        return user != null ? Ok(user) : NotFound();
    }
}