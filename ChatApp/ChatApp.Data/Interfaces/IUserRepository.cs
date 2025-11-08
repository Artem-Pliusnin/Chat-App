using ChatApp.Data.Shared;
using ChatApp.Domain;

namespace ChatApp.Data.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task<IEnumerable<User>> FindUsersByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllAsync();
    
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<OperationResult> DeleteAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string username);
}