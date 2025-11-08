using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[Route("api/chats")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetChatsByUserId(Guid userId)
    {
        try
        {
            var chats = await _chatService.GetChatsByUserIdAsync(userId);
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
