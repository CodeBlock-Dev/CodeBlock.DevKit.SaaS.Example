using HeyItIsMe.Core.Domain.Pages;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.CreatePage;

/// <summary>
/// Use case for creating a new Page entity.
/// This class demonstrates how to implement command handlers that follow CQRS patterns,
/// handle domain events, and integrate with repositories and user context.
/// </summary>
internal class CreatePageUseCase : BaseCommandHandler, IRequestHandler<CreatePageRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;

    /// <summary>
    /// Initializes a new instance of the CreatePageUseCase with the required dependencies.
    /// </summary>
    /// <param name="pageRepository">The repository for page operations</param>
    /// <param name="requestDispatcher">The request dispatcher for handling domain events</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="currentUser">The current user context</param>
    public CreatePageUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<CreatePageUseCase> logger,
        ICurrentUser currentUser
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    /// <summary>
    /// Handles the creation of a new page.
    /// This method demonstrates the complete flow of creating an entity:
    /// 1. Creating the domain entity with proper user context
    /// 2. Persisting to the repository
    /// 3. Publishing domain events for side effects
    /// </summary>
    /// <param name="request">The command request containing the page data</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>A command result with the ID of the created entity</returns>
    public async Task<CommandResult> Handle(CreatePageRequest request, CancellationToken cancellationToken)
    {
        var page = Page.Create(request.Route, request.DisplayName, request.UserId, request.SubscriptionId, _pageRepository);

        await _pageRepository.AddAsync(page);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: page.Id);
    }
}
