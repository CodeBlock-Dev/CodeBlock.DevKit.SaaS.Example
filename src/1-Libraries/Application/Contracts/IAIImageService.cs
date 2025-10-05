using CodeBlock.DevKit.AIChatBot.Domain.Bots;
using CodeBlock.DevKit.Core.Helpers;

namespace HeyItIsMe.Application.Contracts;

public interface IAIImageService
{
    Task<Result<string>> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, int topK = 40);
    Task<Result<string>> GenerateImageAsync(
        LLMParameters parameters,
        IEnumerable<Prompt> prompts,
        IEnumerable<string> referenceImagesBase64,
        int topK = 40
    );
    Task<Result<string>> GenerateImageAsync(LLMParameters parameters, IEnumerable<Prompt> prompts, string referenceImageBase64, int topK = 40);
}
