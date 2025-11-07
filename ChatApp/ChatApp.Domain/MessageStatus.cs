namespace ChatApp.Domain;

public class MessageStatus
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid MessageId { get; set; }
    public bool IsRead { get; set; }
    
    public User User { get; set; }
    public Message Message { get; set; }
}