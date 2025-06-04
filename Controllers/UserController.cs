using ConsoleProject.NET.Contract;

using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UserController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public ActionResult<JwtTokenVm> SignUp([FromBody] SignUpDto dto)
    {
        var token = _authService.SignUp(dto);
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<JwtTokenVm> LogIn([FromBody] LogInDto dto)
    {
        var token = _authService.LogIn(dto);
        if (token is null)
            return NotFound();

        return Ok(token);
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