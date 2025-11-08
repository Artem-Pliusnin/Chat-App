using ChatApp.Domain;

namespace ChatApp.API.Interfaces;

public interface IUserService
{
    Task<User?> GetCurrentUserInfo(string email);

    Task<IEnumerable<User>> FindUsers(string username, Guid[] excludeIds);
}