using Api.Data;
using Api.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
 [Authorize]
[ApiController]
[Route("api/[controller]")] // api/users
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    public UserController(DataContext context)
    {
        _context =context;
    }
    [AllowAnonymous]

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users=await _context.AppUsers.ToListAsync();
        return users;
    }
    [HttpGet("{id}")] // api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.AppUsers.FindAsync(id);
    }
}