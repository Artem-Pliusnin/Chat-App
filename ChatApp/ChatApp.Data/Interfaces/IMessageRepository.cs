using ChatApp.Data.Shared;
using ChatApp.Domain;

namespace ChatApp.Data.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Message>> GetByChatIDAsync(Guid chatId);
    
    Task<IEnumerable<Message>> GetAllAsync();
    
    Task<Message> CreateAsync(Message user);
    Task<Message?> UpdateAsync(Message user);
    Task<OperationResult> DeleteAsync(Guid id);
}