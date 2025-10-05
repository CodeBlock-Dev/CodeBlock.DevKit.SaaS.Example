using CodeBlock.DevKit.AIChatBot.Domain.Bots;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Contracts;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.GenerateFact;

internal class GenerateFactUseCase : BaseCommandHandler, IRequestHandler<GenerateFactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly IAITextService _aiTextService;
    private readonly IAIImageService _aiImageService;
    private readonly IBotRepository _botRepository;

    public GenerateFactUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<GenerateFactUseCase> logger,
        IAITextService aiTextService,
        IAIImageService aiImageService,
        IBotRepository botRepository
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _aiTextService = aiTextService;
        _aiImageService = aiImageService;
        _botRepository = botRepository;
    }

    public async Task<CommandResult> Handle(GenerateFactRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        var loadedVersion = page.Version;

        var fact = await GenerateFactAndAddItToPage(page, request);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: fact.Id);
    }

    private async Task<Fact> GenerateFactAndAddItToPage(Page page, GenerateFactRequest request)
    {
        var textBot = await _botRepository.GetBySystemName("Fact_Text_Generator_Bot");
        var imageBot = await _botRepository.GetBySystemName("Fact_Image_Generator_Bot");

        var content = await GetFactContent(textBot, request.Question, request.Answer);
        var title = await GetFactTitle(textBot, request.Question, request.Answer, content);
        var imageUrl = await GetFactImageUrl(page, imageBot, request.Question, request.Answer, title, content);

        var fact = page.AddFact(title, content);

        page.UpdateFactImageUrl(fact.Id, imageUrl);

        return fact;
    }

    private async Task<string> GetFactContent(Bot bot, string question, string answer)
    {
        var prompts = bot.Prompts;
        prompts.Add(Prompt.Create(question, PromptType.Assistant, "", prompts.Max(p => p.Order) + 1, PromptStatus.Active, 0));
        prompts.Add(Prompt.Create(answer, PromptType.User, "", prompts.Max(p => p.Order) + 2, PromptStatus.Active, 0));

        // TODO: add a hardcoded prompt to create the content

        var result = await _aiTextService.GenerateTextAsync(bot.LLMParameters, prompts);

        return result.Value;
    }

    private async Task<string> GetFactTitle(Bot bot, string question, string answer, string content)
    {
        var prompts = bot.Prompts;
        prompts.Add(Prompt.Create(question, PromptType.Assistant, "", prompts.Max(p => p.Order) + 1, PromptStatus.Active, 0));
        prompts.Add(Prompt.Create(answer, PromptType.User, "", prompts.Max(p => p.Order) + 2, PromptStatus.Active, 0));
        prompts.Add(Prompt.Create(content, PromptType.User, "", prompts.Max(p => p.Order) + 2, PromptStatus.Active, 0));

        // TODO: add a hardcoded prompt to create the title

        var result = await _aiTextService.GenerateTextAsync(bot.LLMParameters, prompts);

        return result.Value;
    }

    private async Task<string> GetFactImageUrl(Page page, Bot bot, string question, string answer, string title, string content)
    {
        var prompts = bot.Prompts;
        prompts.Add(Prompt.Create(question, PromptType.Assistant, "", prompts.Max(p => p.Order) + 1, PromptStatus.Active, 0));
        prompts.Add(Prompt.Create(answer, PromptType.User, "", prompts.Max(p => p.Order) + 2, PromptStatus.Active, 0));
        prompts.Add(Prompt.Create(content, PromptType.User, "", prompts.Max(p => p.Order) + 2, PromptStatus.Active, 0));

        // TODO: add a hardcoded prompt to create the title
        // TODO: convert page avatar to base64 and add it as reference image and use it to generate the image
        var result = await _aiImageService.GenerateImageAsync(bot.LLMParameters, prompts);

        return result.Value;
    }
}
