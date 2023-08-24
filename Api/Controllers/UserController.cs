using System.Net;
using System.Security.Claims;
using Api.Data;
using Api.DTOs;
using Api.Entites;
using Api.Extensions;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;
[ApiController]
[Route("api/[controller]")] // api/users
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserController(IUserRepository userRepository,IMapper mapper,IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoService = photoService;
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        
        
        var user=await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if(user==null) return NotFound();

        _mapper.Map(memberUpdateDto,user);

        if(await _userRepository.SaveAllAsync()) return NoContent();


        return BadRequest("Failed to update user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user =await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if(user==null) return NotFound();

        var result= await _photoService.AddPhotoAsync(file);
        if(result.Error !=null) return BadRequest(result.Error.Message);

        var photo = new Photo()
        {
            Url=result.SecureUrl.AbsoluteUri,
            PublicId= result.PublicId
        };

        if(user.Photos.Count==0) photo.IsMain= true;
        user.Photos.Add(photo);

        
        if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
        

        return BadRequest("Problem Adding Photo.");
    }
}