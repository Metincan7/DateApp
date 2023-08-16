using Api.Data;
using Api.Entites;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")] // api/users
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [AllowAnonymous]

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }
    [HttpGet("{username}")] // api/users/2
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }
}