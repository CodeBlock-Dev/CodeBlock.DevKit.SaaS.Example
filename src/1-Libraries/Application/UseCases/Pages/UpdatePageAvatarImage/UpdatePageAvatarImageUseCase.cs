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

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePageAvatarImage;

internal class UpdatePageAvatarImageUseCase : BaseCommandHandler, IRequestHandler<UpdatePageAvatarImageRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IImageService _imageService;

    public UpdatePageAvatarImageUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdatePageAvatarImageUseCase> logger,
        ICurrentUser currentUser,
        IImageService imageService
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
        _imageService = imageService;
    }

    public async Task<CommandResult> Handle(UpdatePageAvatarImageRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var loadedVersion = page.Version;

        var fileName = $"avatar.jpg";
        var imageUrl = await _imageService.SaveImageFileAsync(fileName, request.Base64Image, "pages", request.Id);
        
        // Add cache-busting query parameter to the URL
        imageUrl = $"{imageUrl}?v={RandomDataGenerator.GetRandomNumber(5)}";

        page.UpdateAvatarImageUrl(imageUrl);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: page.Id);
    }
}
