using System.Security.Claims;
using AutoMapper;
using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    

    public UserController(IMapper mapper ,IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine($"Authorization header: {authHeader}");

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }
            
            var user = await _userService.GetCurrentUserInfo(email);
            if (user == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<UserDto>(user);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error while getting user info",
                error = ex.Message
            });
        }
    }
    
    [Authorize]
    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string username, [FromQuery] Guid[] excludeIds)
    {
        try
        {
            var users = await _userService.FindUsers(username, excludeIds);

            var response = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error while searching users",
                error = ex.Message
            });
        }
    }
}