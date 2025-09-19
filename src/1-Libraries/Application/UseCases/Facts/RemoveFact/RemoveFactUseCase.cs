using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.RemoveFact;

internal class RemoveFactUseCase : BaseCommandHandler, IRequestHandler<RemoveFactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    public RemoveFactUseCase(IPageRepository pageRepository, IRequestDispatcher requestDispatcher, ILogger<RemoveFactUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

    public async Task<CommandResult> Handle(RemoveFactRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        var loadedVersion = page.Version;

        page.RemoveFact(request.FactId);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: request.FactId);
    }
}
