using Microsoft.Extensions.AI;
using System.Text.Json;

namespace Hivify.AI.Services;

public sealed class FeedGenerator : IFeedGenerator
{
    private readonly IChatClient _client;

    public FeedGenerator(IChatClient client)
    {
        _client = client;
    }

    public async Task<GeneratedFeedDto> GenerateAsync(
        string instruction)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(instruction);

        var prompt = $$"""
            You are a municipality communication assistant.

            Your task is to create a public announcement feed for citizens.

            Requirements:
            - Create a clear and informative title.
            - Write professional and easy-to-understand content.
            - The announcement must be suitable for citizens.
            - Do not invent facts or add information that is not provided.
            - If information is missing, keep the wording general.

            User request:
            {{instruction}}

            Return only valid JSON.
            Do not use markdown.
            Do not use ```json.
            Do not add any text outside the JSON.

            JSON format:

            {
              "title": "",
              "content": ""
            }
            """;

        var response = await _client.GetResponseAsync(prompt);

        var json = response.Text;

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new InvalidOperationException(
                "The AI returned an empty response.");
        }

        try
        {
            var result =
                JsonSerializer.Deserialize<GeneratedFeedDto>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (result is null)
            {
                throw new InvalidOperationException(
                    "The AI response could not be converted to a feed.");
            }

            if (string.IsNullOrWhiteSpace(result.Title) ||
                string.IsNullOrWhiteSpace(result.Content))
            {
                throw new InvalidOperationException(
                    "The AI returned an incomplete feed.");
            }

            return result;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "The AI returned invalid feed JSON.",
                ex);
        }
    }
}