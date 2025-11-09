using System.Security.Claims;
using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Controllers;

[Route("api/messages")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessageController(
        IMessageService messageService, 
        IUserService userService,
        IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _userService = userService;
        _hubContext = hubContext;
    }
    [Authorize]
    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetMessagesByChatId(Guid chatId)
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
        
        var result = await _messageService.GetMessagesByChatIdAsync(chatId);
        
        var messagesId = result.Select(r => r.Id);
        
        await _messageService.UpdateStatusesForMessages(messagesId, currentUser.Id);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("{messageId}/mark-as-read")]
    public async Task<IActionResult> MarkAsRead(Guid messageId)
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

        await _messageService.UpdateStatusForMessage(messageId, currentUser.Id);

        return Ok(new { Message = "Marked as read" });
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
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

        try
        {
            var result = await _messageService.CreateMessageAsync(dto, currentUser.Id);
            
            var groupName = $"chat_{dto.ChatId}";
            
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", result);

            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error while creating message",
                error = ex.Message
            });
        }
    }
}