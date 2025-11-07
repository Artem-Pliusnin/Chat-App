using ChatApp.API.Models;
using ChatApp.Domain;

namespace ChatApp.API.Interfaces;

public interface IAuthService
{
    Task<SignUpResult> SignUpAsync(User user);
    
    Task<SignInResult> SignInAsync(User user);
}