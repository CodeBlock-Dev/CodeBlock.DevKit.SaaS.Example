using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.GenerateFact;

internal class GenerateFactUseCase : BaseCommandHandler, IRequestHandler<GenerateFactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    public GenerateFactUseCase(IPageRepository pageRepository, IRequestDispatcher requestDispatcher, ILogger<GenerateFactUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

    public async Task<CommandResult> Handle(GenerateFactRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        var loadedVersion = page.Version;

        var content = await GetFactContent(request);
        var title = await GetFactTitle(request, content);
        var imageUrl = await GetFactImageUrl(request, title, content);

        var fact = page.AddFact(title, content);
        page.UpdateFactImageUrl(fact.Id, imageUrl);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: fact.Id);
    }

    private async Task<string> GetFactImageUrl(GenerateFactRequest request, string title, string content)
    {
        return await Task.FromResult(string.Empty);
    }

    private async Task<string> GetFactTitle(GenerateFactRequest request, string content)
    {
        return await Task.FromResult(string.Empty);
    }

    private async Task<string> GetFactContent(GenerateFactRequest request)
    {
        return await Task.FromResult(string.Empty);
    }
}
