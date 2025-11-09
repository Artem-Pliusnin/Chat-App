using Azure.AI.TextAnalytics;

namespace ChatApp.API.Models.DTOs;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public UserDto Sender { get; set; }
    public Guid ChatId { get; set; }
    public DateTime SendDate { get; set; }
    
    public TextSentiment Sentiment { get; set; }
}