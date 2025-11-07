using AutoMapper;
using ChatApp.API.Interfaces;
using ChatApp.API.Models.DTOs;
using ChatApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IMapper mapper ,IAuthService authService, ITokenService tokenService)
    {
        _mapper = mapper;
        _authService = authService;
        _tokenService = tokenService;
    }
    
    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn(SignInDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Check your information");
        }

        try
        {
            var user = _mapper.Map<User>(dto);
            var result = await _authService.SignInAsync(user);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _tokenService.CreateToken(user);

            var response = new LoginResponseDto()
            {
                JwtToken = token,
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while signing in", error = ex.Message });
        }

    }

    [HttpPost("signUp")]
    public async Task<IActionResult> SignUp(SignUpDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Check your sign up form.");
        }

        var user = _mapper.Map<User>(dto);
        var result = await _authService.SignUpAsync(user);
        if (!result.Succeeded)
        {
            if (result.IsEmailAlreadyExists)
            {
                return BadRequest("Email already taken");
            }
            if (result.IsUserNameAlreadyExists)
            {
                return BadRequest("Username already taken");
            }
            return BadRequest("SignUp failed. Try again.");
        }
        return Ok();
    }
}