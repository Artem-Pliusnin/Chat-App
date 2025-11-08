using ChatApp.API.Models.DTOs;
using ChatApp.Domain;

namespace ChatApp.API.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
    Task<MessageDto> CreateMessageAsync(CreateMessageDto dto, Guid senderId);
}