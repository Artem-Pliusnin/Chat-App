using ChatApp.API.Models.DTOs;

namespace ChatApp.API.Interfaces;

public interface IChatService
{
    Task<IEnumerable<ChatDto>> GetChatsByUserIdAsync(Guid userId);
    Task<ChatDto> CreateChatAsync(NewChatDto dto);
}