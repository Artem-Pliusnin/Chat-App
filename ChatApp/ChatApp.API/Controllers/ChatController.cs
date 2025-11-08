using System.Security.Claims;
using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[Route("api/chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;

    public ChatController(IChatService chatService, IUserService userService)
    {
        _chatService = chatService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<IActionResult> GetChatsByUserId()
    {
        try
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            var currentUser = await _userService.GetCurrentUserInfo(email);
            if (currentUser == null)
            {
                return Unauthorized();
            }
            
            var chats = await _chatService.GetChatsByUserIdAsync(currentUser.Id);
            return Ok(chats);
        }
        catch (Exception ex)
        { 
            return StatusCode(500, new
            {
                message = "Error while getting chats",
                error = ex.Message
            });
        }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] NewChatDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var chat = await _chatService.CreateChatAsync(dto);
            return Ok(chat);
        }
        catch (Exception ex)
        { 
            return StatusCode(500, new
            {
                message = "Error while creating chat",
                error = ex.Message
            });
        }
    }
}
