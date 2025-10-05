using CodeBlock.DevKit.AIChatBot.Domain.Bots;
using CodeBlock.DevKit.Core.Helpers;

namespace HeyItIsMe.Application.Contracts;

public interface IAITextService
{
    Task<Result<string>> GenerateTextAsync(LLMParameters parameters, IEnumerable<Prompt> prompts);
}
