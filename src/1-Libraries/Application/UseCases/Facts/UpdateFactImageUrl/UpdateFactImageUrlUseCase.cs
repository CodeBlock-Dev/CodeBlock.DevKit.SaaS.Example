using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Contracts;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Facts.UpdateFactImageUrl;

internal class UpdateFactImageUrlUseCase : BaseCommandHandler, IRequestHandler<UpdateFactImageUrlRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IImageService _imageService;

    public UpdateFactImageUrlUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdateFactImageUrlUseCase> logger,
        ICurrentUser currentUser,
        IImageService imageService
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
        _imageService = imageService;
    }

    public async Task<CommandResult> Handle(UpdateFactImageUrlRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByFactIdAsync(request.FactId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound("FactId: " + request.FactId);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var loadedVersion = page.Version;

        var fileName = $"{request.FactId}.jpg?v={RandomDataGenerator.GetRandomNumber(5)}";
        var imageUrl = await _imageService.SaveImageFileAsync(fileName, request.Base64Image, "pages", page.Id, "facts");

        page.UpdateFactImageUrl(request.FactId, imageUrl);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: request.FactId);
    }
}
