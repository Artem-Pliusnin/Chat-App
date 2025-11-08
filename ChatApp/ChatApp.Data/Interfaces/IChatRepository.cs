using ChatApp.Data.Shared;
using ChatApp.Domain;

namespace ChatApp.Data.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Chat>> GetByUserIDAsync(Guid userId);
    
    Task<IEnumerable<Chat>> GetAllAsync();
    
    Task<Chat> CreateAsync(Chat user);
    Task<Chat?> UpdateAsync(Chat user);
    Task<OperationResult> DeleteAsync(Guid id);
}