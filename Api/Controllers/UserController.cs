using Api.Data;
using Api.Entites;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")] // api/users
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    [AllowAnonymous]

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users =await _userRepository.GetMembersAsync();


        return Ok(users);   
    }
    [HttpGet("{username}")] // api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        return await  _userRepository.GetMemberAsync(username);

        
    }
}