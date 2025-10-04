using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePage;

internal class UpdatePageUseCase : BaseCommandHandler, IRequestHandler<UpdatePageRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;

    public UpdatePageUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdatePageUseCase> logger,
        ICurrentUser currentUser
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    public async Task<CommandResult> Handle(UpdatePageRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Question.QUESTIONS);

        var loadedVersion = page.Version;

        page.Update(request.Route, request.DisplayName, _pageRepository);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: page.Id);
    }
}
