using Azure.AI.TextAnalytics;

namespace ChatApp.API.Interfaces;

public interface ICognitiveService
{
    Task<TextSentiment> AnalyzeSentimentAsync(string message);
}