using System.Security.Cryptography;
using System.Text;
using ChatApp.API.Interfaces;
using ChatApp.API.Models;
using ChatApp.Data.Interfaces;
using ChatApp.Domain;

namespace ChatApp.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<SignUpResult> SignUpAsync(User user)
    {
        if (await _userRepository.ExistsByEmailAsync(user.Email))
        {
            return SignUpResult.EmailFailure;
        }

        if (await _userRepository.ExistsByUserNameAsync(user.UserName))
        {
            return SignUpResult.UserNameFailure;
        }

        user.Password = HashPassword(user.Password);

        await _userRepository.CreateAsync(user);
        
        return SignUpResult.Success;
    }

    public async Task<SignInResult> SignInAsync(User user)
    {
        var dbUser = await _userRepository.GetByEmailAsync(user.Email);

        if (dbUser == null)
        {
            return SignInResult.Failure;
        }

        var hashedPassword = HashPassword(user.Password);
        if (dbUser.Password != hashedPassword)
        {
            return SignInResult.Failure;
        }
        
        return SignInResult.Success;
    }
    
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
    
}