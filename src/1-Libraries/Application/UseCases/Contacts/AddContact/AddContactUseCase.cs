using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Contacts.AddContact;

/// <summary>
/// Use case for adding a new Contact to a Page.
/// This class demonstrates how to implement command handlers that include:
/// - Entity existence validation with custom exceptions
/// - Concurrency-safe updates using version control
/// - Domain event publishing for side effects
/// - Proper error handling patterns
/// </summary>
internal class AddContactUseCase : BaseCommandHandler, IRequestHandler<AddContactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    /// <summary>
    /// Initializes a new instance of the AddContactUseCase with the required dependencies.
    /// </summary>
    /// <param name="pageRepository">The repository for page operations</param>
    /// <param name="requestDispatcher">The request dispatcher for handling domain events</param>
    /// <param name="logger">The logger for this use case</param>
    public AddContactUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<AddContactUseCase> logger
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

    /// <summary>
    /// Handles the addition of a new contact to a page.
    /// This method demonstrates a complete flow including:
    /// 1. Entity retrieval and existence validation
    /// 2. Concurrency-safe updates using version control
    /// 3. Domain event publishing for side effects
    /// 4. Proper error handling with custom exceptions
    /// </summary>
    /// <param name="request">The command request containing the contact data</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>A command result with the ID of the created contact</returns>
    /// <exception cref="PageApplicationExceptions">Thrown when the page is not found</exception>
    public async Task<CommandResult> Handle(AddContactRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.PageId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.PageId);

        var loadedVersion = page.Version;

        var contact = page.AddContact(request.Content);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: contact.Id);
    }
}
