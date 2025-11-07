namespace ChatApp.Domain;

public class Message
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ChatId { get; set; }
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    
    public User Sender { get; set; }
    public Chat Chat { get; set; }
    public List<MessageStatus> MessageStatuses { get; set; } = new List<MessageStatus>() { };
}