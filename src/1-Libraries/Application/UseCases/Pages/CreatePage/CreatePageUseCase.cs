using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.CreatePage;

internal class CreatePageUseCase : BaseCommandHandler, IRequestHandler<CreatePageRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;

    public CreatePageUseCase(IPageRepository pageRepository, IRequestDispatcher requestDispatcher, ILogger<CreatePageUseCase> logger)
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
    }

    public async Task<CommandResult> Handle(CreatePageRequest request, CancellationToken cancellationToken)
    {
        var page = Page.Create(request.Route, request.DisplayName, request.UserId, _pageRepository);

        await _pageRepository.AddAsync(page);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: page.Id);
    }
}
