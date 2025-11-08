using ChatApp.API.Models.DTOs;
using ChatApp.Domain;

namespace ChatApp.API.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
    Task<Message> CreateMessageAsync(CreateMessageDto dto, Guid senderId);
}