using System.ComponentModel.DataAnnotations;

namespace ChatApp.API.Models.DTOs;

public class CreateMessageDto
{
    [Required]
    public Guid ChatId { get; set; }
    
    [MinLength(1)]
    [Required]
    public string Text { get; set; }
}