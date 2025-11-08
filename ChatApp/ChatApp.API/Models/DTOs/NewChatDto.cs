using System.ComponentModel.DataAnnotations;

namespace ChatApp.API.Models.DTOs;

public class NewChatDto
{
    [Required]
    [MaxLength(30)]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    [MinLength(2)]
    public Guid[] MemberIds { get; set; }
}