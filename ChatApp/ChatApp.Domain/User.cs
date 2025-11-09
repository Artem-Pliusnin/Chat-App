using Azure.AI.TextAnalytics;

namespace ChatApp.Domain;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<ChatMember> Chats { get; set; } = new List<ChatMember>() { };
    public List<Message> Messages { get; set; } = new List<Message>() { };
    public List<MessageStatus> MessageStatuses { get; set; } = new List<MessageStatus>() { };
}