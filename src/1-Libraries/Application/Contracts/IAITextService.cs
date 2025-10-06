using CodeBlock.DevKit.AIChatBot.Domain.Bots;

namespace HeyItIsMe.Application.Contracts;

public interface IAITextService
{
    Task<string> GenerateTextAsync(LLMParameters parameters, IEnumerable<Prompt> prompts);
}
