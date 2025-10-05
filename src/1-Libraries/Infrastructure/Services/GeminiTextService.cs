using System.Text;
using System.Text.Json;
using CodeBlock.DevKit.AIChatBot.Domain.Bots;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Contracts;
using HeyItIsMe.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Infrastructure.Services;

public class GeminiTextService : IAITextService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GeminiTextService> _logger;
    private readonly IEncryptionService _encryptionService;

    public GeminiTextService(HttpClient httpClient, ILogger<GeminiTextService> logger, IEncryptionService encryptionService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _encryptionService = encryptionService;
    }

    public async Task<Result<string>> GenerateTextAsync(LLMParameters parameters, IEnumerable<Prompt> prompts)
    {
        var contents = new List<object>();

        foreach (var prompt in prompts.OrderBy(p => p.Order))
        {
            var role = MapPromptTypeToRole(prompt.Type);
            contents.Add(new { role, parts = new[] { new { text = prompt.Content } } });
        }

        return await GenerateTextInternalAsync(
            parameters.ApiKey,
            parameters.ModelName,
            contents.ToArray(),
            parameters.Temperature,
            parameters.TopP,
            parameters.MaxOutputTokens
        );
    }

    private string MapPromptTypeToRole(PromptType promptType)
    {
        return promptType switch
        {
            PromptType.User => "user",
            PromptType.Developer => "developer",
            PromptType.System => "system",
            PromptType.Assistant => "model",
            _ => "user",
        };
    }

    private async Task<Result<string>> GenerateTextInternalAsync(
        string apiKey,
        string model,
        object[] contents,
        double? temperature = 0.7,
        double? topP = 0.95,
        int? maxOutputTokens = 1024
    )
    {
        try
        {
            // Using Gemini models for text generation

            var requestBody = new
            {
                contents,
                generationConfig = new
                {
                    temperature,
                    topP,
                    maxOutputTokens,
                },
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();

            var decryptedApiKey = _encryptionService.DecryptText(apiKey);

            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", decryptedApiKey);

            var response = await _httpClient.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var geminiResponse = JsonSerializer.Deserialize<GeminiTextResponse>(responseContent);

                if (geminiResponse?.Candidates?.Length > 0)
                {
                    var candidate = geminiResponse.Candidates[0];
                    if (candidate.Content?.Parts?.Length > 0)
                    {
                        foreach (var part in candidate.Content.Parts)
                        {
                            if (!string.IsNullOrEmpty(part.Text))
                            {
                                var generatedText = part.Text;
                                return Result.Success<string>(generatedText);
                            }
                        }
                    }
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Gemini Text API error: {StatusCode} - {Content}", response.StatusCode, errorContent);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Gemini Text API");
        }

        return Result.Failure<string>();
    }
}
