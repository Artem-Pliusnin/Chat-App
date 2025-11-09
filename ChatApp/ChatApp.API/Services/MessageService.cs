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
    private readonly ICognitiveService _cognitiveService;

    public MessageService(
        IMessageRepository messageRepository,
        IMessageStatusRepository statusRepository,
        IChatRepository chatRepository, 
        ICognitiveService cognitiveService)
    {
        _messageRepository = messageRepository;
        _statusRepository = statusRepository;
        _chatRepository = chatRepository;
        _cognitiveService = cognitiveService;
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(Guid chatId)
    {
        var messages = await _messageRepository.GetByChatIDAsync(chatId);

        return messages.Select(m => new MessageDto
        {
            Id = m.Id,
            Text = m.Text,
            Sender = new UserDto()
            {
                Id = m.Sender.Id,
                UserName = m.Sender.UserName,
            },
            ChatId = m.ChatId,
            SendDate = m.TimeStamp,
            Sentiment = m.Sentiment,
        }).ToList();
    }

    public async Task<MessageDto> CreateMessageAsync(CreateMessageDto dto, Guid senderId)
    {
        try
        {
            var chat = await _chatRepository.GetByIdAsync(dto.ChatId);
            if (chat == null)
            {
                throw new NullReferenceException("Chat not found");
            }
            
            var sentiment = await _cognitiveService.AnalyzeSentimentAsync(dto.Text);

            var message = new Message
            {
                ChatId = dto.ChatId,
                SenderId = senderId,
                Text = dto.Text,
                TimeStamp = DateTime.UtcNow,
                Sentiment = sentiment,
            };

            await _messageRepository.CreateAsync(message);

            var userIds = chat.Members.Select(m => m.UserId).ToList();
            await _statusRepository.CreateStatusesForMessage(message.Id, userIds);

            return new MessageDto()
            {
                Id = message.Id,
                ChatId = message.ChatId,
                Sender = new UserDto()
                {
                    Id = message.Sender.Id,
                    UserName = message.Sender.UserName,
                },
                Text = message.Text,
                SendDate = message.TimeStamp,
                Sentiment = message.Sentiment,
            };
        }
        catch (Exception ex)
        {
            throw new($"Error creating message: {ex.Message}");
        }
    }
}