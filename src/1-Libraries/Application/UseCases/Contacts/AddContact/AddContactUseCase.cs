using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Contacts.AddContact;

internal class AddContactUseCase : BaseCommandHandler, IRequestHandler<AddContactRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    public AddContactUseCase(IPageRepository pageRepository, IRequestDispatcher requestDispatcher, ILogger<AddContactUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

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
