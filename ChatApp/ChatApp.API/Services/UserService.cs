using ChatApp.API.Interfaces;
using ChatApp.Data.Interfaces;
using ChatApp.Domain;

namespace ChatApp.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetCurrentUserInfo(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }
    
    public async Task<IEnumerable<User>> FindUsers(string username, Guid[] excludeIds)
    {
        var users = await _userRepository.FindUsersByUsernameAsync(username);

        if (excludeIds.Length != 0)
        {
            users = users
                .Where(u => !excludeIds.Contains(u.Id));
        }

        return users;
    }
}