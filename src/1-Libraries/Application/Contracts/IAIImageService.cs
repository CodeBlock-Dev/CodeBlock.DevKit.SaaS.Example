using CodeBlock.DevKit.AIChatBot.Domain.Bots;

namespace HeyItIsMe.Application.Contracts;

public interface IAIImageService
{
    Task<string> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, int topK = 40);
    Task<string> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, IEnumerable<string> referenceImagesBase64, int topK = 40);
    Task<string> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, string referenceImageBase64, int topK = 40);
}
