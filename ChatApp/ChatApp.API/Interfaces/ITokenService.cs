using ChatApp.Domain;

namespace ChatApp.API.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}