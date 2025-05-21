using Microsoft.AspNetCore.Mvc;
using ConsoleProject.NET.Exceptions;
using ConsoleProject.NET.Models;
using ConsoleProject.NET.Repositories;
using ConsoleProject.NET.Contract;

namespace ConsoleProject.NET.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<UserVm>> GetUsers()
      => Ok(repository.GetUsers());

    [HttpPost]
    public ActionResult<int> Create([FromBody] UserAddDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = repository.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }
    [HttpGet("{id}")]
    public ActionResult<UserVm> GetById(int id) 
    {
        var user = repository.GetById(id);
        return user != null ? Ok(user) : NotFound();
    }
}