using System.Security.Claims;
using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.API.Shared;
using ChatApp.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.API.Controllers;


[Authorize]
public class ChatHub : Hub
{
    private readonly IMessageService _messageService;
    private readonly IChatService _chatService;
    private readonly IUserService _userService;
    private readonly IChatRepository _chatRepository;

    public ChatHub(
        IMessageService messageService,
        IChatService chatService,
        IUserService userService,
        IChatRepository chatRepository)
    {
        _messageService = messageService;
        _chatService = chatService;
        _userService = userService;
        _chatRepository = chatRepository;
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = await GetUserId();
        
        if (userId == null)
        {
            await base.OnConnectedAsync();
            return;
        }
        
        ConnectionMapping.Add(userId.Value, Context.ConnectionId);

        try
        {
            var userChats = await _chatRepository.GetByUserIDAsync(userId.Value);

            foreach (var chat in userChats)
            {
                var groupName = GetGroupName(chat.Id);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnConnectedAsync for user {userId}");
        }

        await base.OnConnectedAsync();
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = await GetUserId();
        if (userId != null)
        {
            ConnectionMapping.Remove(userId.Value, Context.ConnectionId);
        }

        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task SendMessage(MessageDto messageDto)
    {
        try
        {
            var groupName = GetGroupName(messageDto.ChatId);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", messageDto);
        }
        catch (Exception ex)
        {
            throw new HubException($"Failed to broadcast message: {ex.Message}");
        }
    }

    private async Task<Guid?> GetUserId()
    {
        var email = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (!string.IsNullOrEmpty(email))
        {
            var currentUser = await _userService.GetCurrentUserInfo(email);
            return currentUser?.Id;
        }

        return null;
    }

    private static string GetGroupName(Guid chatId)
    {
        return $"chat_{chatId}";
    }
    
}