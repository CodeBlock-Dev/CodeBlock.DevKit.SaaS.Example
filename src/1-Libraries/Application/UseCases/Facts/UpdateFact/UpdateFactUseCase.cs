using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFact;

/// <summary>
/// Use case for updating an existing Fact on a Page.
/// This class demonstrates how to implement update command handlers that include:
/// - Entity existence validation with custom exceptions
/// - Concurrency-safe updates using version control
/// - Domain event publishing for side effects
/// - Proper error handling patterns
/// </summary>
internal class UpdateFactUseCase : BaseCommandHandler, IRequestHandler<UpdateFactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    /// <summary>
    /// Initializes a new instance of the UpdateFactUseCase with the required dependencies.
    /// </summary>
    /// <param name="pageRepository">The repository for page operations</param>
    /// <param name="requestDispatcher">The request dispatcher for handling domain events</param>
    /// <param name="logger">The logger for this use case</param>
    public UpdateFactUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdateFactUseCase> logger
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

    /// <summary>
    /// Handles the update of an existing fact on a page.
    /// This method demonstrates a complete update flow including:
    /// 1. Entity retrieval and existence validation
    /// 2. Concurrency-safe updates using version control
    /// 3. Domain event publishing for side effects
    /// 4. Proper error handling with custom exceptions
    /// </summary>
    /// <param name="request">The command request containing the update data</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>A command result with the ID of the updated fact</returns>
    /// <exception cref="PageApplicationExceptions">Thrown when the page is not found</exception>
    public async Task<CommandResult> Handle(UpdateFactRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        var loadedVersion = page.Version;

        page.UpdateFact(request.FactId, request.Content);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: request.FactId);
    }
}
