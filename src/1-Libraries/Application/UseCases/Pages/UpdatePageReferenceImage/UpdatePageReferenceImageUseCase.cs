using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.UpdatePageReferenceImage;

internal class UpdatePageReferenceImageUseCase : BaseCommandHandler, IRequestHandler<UpdatePageReferenceImageRequest, CommandResult>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;

    public UpdatePageReferenceImageUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdatePageReferenceImageUseCase> logger,
        ICurrentUser currentUser
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    public async Task<CommandResult> Handle(UpdatePageReferenceImageRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var loadedVersion = page.Version;

        var imageUrl = await SaveImageFileAsync(request.Id, request.Base64Image, request.WebRootPath, "reference");

        page.UpdateReferenceImageUrl(imageUrl);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: page.Id);
    }

    private async Task<string> SaveImageFileAsync(string pageId, string base64Image, string webRootPath, string imageType)
    {
        var uploadsFolder = Path.Combine(webRootPath, "pages", pageId);
        
        Directory.CreateDirectory(uploadsFolder);

        var imageData = Convert.FromBase64String(base64Image);
        var fileName = $"{imageType}_{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await File.WriteAllBytesAsync(filePath, imageData);

        return $"/pages/{pageId}/{fileName}";
    }
}
