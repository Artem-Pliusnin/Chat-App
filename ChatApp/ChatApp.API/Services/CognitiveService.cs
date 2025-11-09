using Azure.AI.TextAnalytics;
using ChatApp.API.Interfaces;

namespace ChatApp.API.Services;

public class CognitiveService : ICognitiveService
{
    private readonly TextAnalyticsClient _client;

    public CognitiveService(TextAnalyticsClient client)
    {
        _client = client;
    }

    public async Task<TextSentiment> AnalyzeSentimentAsync(string message)
    {
        var response = await _client.AnalyzeSentimentAsync(message);
        var sentiment = response.Value.Sentiment;
        return sentiment;
    }
}