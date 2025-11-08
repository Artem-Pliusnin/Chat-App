using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.Data.Interfaces;
using ChatApp.Domain;

namespace ChatApp.API.Services;

public class ChatService: IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<ChatDto>> GetChatsByUserIdAsync(Guid userId)
        {
            var chats = await _chatRepository.GetByUserIDAsync(userId);

            var chatDtos = chats
                .Select(c =>
                {
                    var lastMessage = c.Messages
                        .OrderByDescending(m => m.TimeStamp)
                        .FirstOrDefault();

                    return new ChatDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        LastMessage = lastMessage?.Text ?? ""
                    };
                }).ToList();

            return chatDtos;
        }

        public async Task<ChatDto> CreateChatAsync(NewChatDto dto)
        {
            var chat = new Chat
            {
                Name = dto.Name,
                TimeStamp = DateTime.UtcNow,
                Members = dto.MemberIds
                    .Select(id => new ChatMember
                    {
                        UserId = id
                    }).ToList()
            };
            
            var createdChat = await _chatRepository.CreateAsync(chat);

            return new ChatDto
            {
                Id = createdChat.Id,
                Name = createdChat.Name,
                LastMessage = ""
            };
        }
}