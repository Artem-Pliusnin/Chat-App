namespace ChatApp.Domain;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime TimeStamp { get; set; }
    
    public List<ChatMember> Members { get; set; } = new List<ChatMember>() { };
    public List<Message> Messages { get; set; } = new List<Message>() { };
}