namespace ChatApp.API.Models.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastMessage { get; set; }
    public bool HasUnreadMessages { get; set; }
}