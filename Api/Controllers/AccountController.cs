using System.Text;
using System.Security.Cryptography;
using Api.Data;
using Api.Entites;
using Microsoft.AspNetCore.Mvc;
using Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest ("Username is taken.");
            using var hmac=new HMACSHA512();

            var user =new AppUser
            {
                    UserName = registerDto.UserName.ToLower(),
                    PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                    PasswordSalt=hmac.Key
            };
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user =await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName==loginDto.UserName);
            if (user==null) return Unauthorized ("invalid username");

            using var hmac=new HMACSHA512(user.PasswordSalt);
            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i=0;i < computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i] ) return Unauthorized("invalid password");
            }
            return Ok();

        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.AppUsers.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}