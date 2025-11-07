using System.ComponentModel.DataAnnotations;

namespace ChatApp.API.Models.DTOs;

public class SignUpDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}