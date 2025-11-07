namespace ChatApp.Domain;

public class ChatMember
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }

    public Chat Chat { get; set; }
    public User User { get; set; }
}