using System.Text;
using System.Text.Json;
using CodeBlock.DevKit.AIChatBot.Domain.Bots;
using CodeBlock.DevKit.Application.Srvices;
using HeyItIsMe.Application.Contracts;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Infrastructure.Services;

public class GeminiImageService : IAIImageService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GeminiImageService> _logger;
    private readonly IEncryptionService _encryptionService;

    public GeminiImageService(HttpClient httpClient, ILogger<GeminiImageService> logger, IEncryptionService encryptionService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _encryptionService = encryptionService;
    }

    public async Task<string> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, int topK = 40)
    {
        var parts = new List<object>();

        foreach (var prompt in prompts.OrderBy(p => p.Order))
            parts.Add(new { text = prompt.Content });

        return await GenerateImageInternalAsync(
            parameters.ApiKey,
            parameters.ModelName,
            parts.ToArray(),
            parameters.Temperature,
            topK,
            parameters.TopP,
            parameters.MaxOutputTokens
        );
    }

    public async Task<string> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, string referenceImageBase64, int topK = 40)
    {
        var parts = new List<object>();

        foreach (var prompt in prompts.OrderBy(p => p.Order))
            parts.Add(new { text = prompt.Content });

        parts.Add(new { inlineData = new { mimeType = "image/jpeg", data = referenceImageBase64 } });

        return await GenerateImageInternalAsync(
            parameters.ApiKey,
            parameters.ModelName,
            parts.ToArray(),
            parameters.Temperature,
            topK,
            parameters.TopP,
            parameters.MaxOutputTokens
        );
    }

    public async Task<string> GenerateImageAsync(
        LLMParameters parameters,
        IEnumerable<Prompt> prompts,
        IEnumerable<string> referenceImagesBase64,
        int topK = 40
    )
    {
        var parts = new List<object>();

        foreach (var prompt in prompts.OrderBy(p => p.Order))
            parts.Add(new { text = prompt.Content });

        foreach (var imageBase64 in referenceImagesBase64)
            parts.Add(new { inlineData = new { mimeType = "image/jpeg", data = imageBase64 } });

        return await GenerateImageInternalAsync(
            parameters.ApiKey,
            parameters.ModelName,
            parts.ToArray(),
            parameters.Temperature,
            topK,
            parameters.TopP,
            parameters.MaxOutputTokens
        );
    }

    private async Task<string> GenerateImageInternalAsync(
        string apiKey,
        string model,
        object[] parts,
        double? temperature = 0.7,
        int? topK = 40,
        double? topP = 0.95,
        int? maxOutputTokens = 1024
    )
    {
        try
        {
            // Using Gemini 2.5 Flash Image (Nano Banana) for image generation

            var requestBody = new
            {
                contents = new[] { new { parts } },
                generationConfig = new
                {
                    temperature,
                    topK,
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
                var geminiResponse = JsonSerializer.Deserialize<GeminiImageResponse>(responseContent);

                if (geminiResponse?.Candidates?.Length > 0)
                {
                    var candidate = geminiResponse.Candidates[0];
                    if (candidate.Content?.Parts?.Length > 0)
                    {
                        foreach (var part in candidate.Content.Parts)
                        {
                            if (!string.IsNullOrEmpty(part.InlineData?.Data))
                            {
                                var base64Image = part.InlineData.Data;
                                return base64Image;
                            }
                        }
                    }
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Gemini 2.5 Flash Image API error: {StatusCode} - {Content}", response.StatusCode, errorContent);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Gemini 2.5 Flash Image API");
        }

        throw ApplicationExceptions.AIResponseFailed();
    }
}
