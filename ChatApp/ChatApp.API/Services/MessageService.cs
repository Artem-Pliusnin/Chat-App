using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.Data.Interfaces;
using ChatApp.Domain;

namespace ChatApp.API.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageStatusRepository _statusRepository;
    private readonly IChatRepository _chatRepository;

    public MessageService(
        IMessageRepository messageRepository,
        IMessageStatusRepository statusRepository,
        IChatRepository chatRepository)
    {
        _messageRepository = messageRepository;
        _statusRepository = statusRepository;
        _chatRepository = chatRepository;
    }

    public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
    {
        return await _messageRepository.GetByChatIDAsync(chatId);
    }

    public async Task<Message> CreateMessageAsync(CreateMessageDto dto, Guid senderId)
    {
        try
        {
            var chat = await _chatRepository.GetByIdAsync(dto.ChatId);
            if (chat == null)
            {
                throw new NullReferenceException("Chat not found");
            }

            var message = new Message
            {
                ChatId = dto.ChatId,
                SenderId = senderId,
                Text = dto.Text,
                TimeStamp = DateTime.UtcNow
            };

            await _messageRepository.CreateAsync(message);

            var userIds = chat.Members.Select(m => m.UserId).ToList();
            await _statusRepository.CreateStatusesForMessage(message.Id, userIds);

            return message;
        }
        catch (Exception ex)
        {
            throw new($"Error creating message: {ex.Message}");
        }
    }
}