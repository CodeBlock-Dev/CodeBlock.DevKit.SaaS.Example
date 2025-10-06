using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Core.Helpers;
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

    public UpdateFactImageUrlUseCase(
        IPageRepository pageRepository,
        IRequestDispatcher requestDispatcher,
        ILogger<UpdateFactImageUrlUseCase> logger,
        ICurrentUser currentUser
    )
        : base(requestDispatcher, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
    }

    public async Task<CommandResult> Handle(UpdateFactImageUrlRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByFactIdAsync(request.FactId);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound("FactId: " + request.FactId);

        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var loadedVersion = page.Version;

        var imageUrl = await SaveImageFileAsync(page.Id, request.FactId, request.Base64Image, request.WebRootPath);

        page.UpdateFactImageUrl(request.FactId, imageUrl);

        await _pageRepository.ConcurrencySafeUpdateAsync(page, loadedVersion);

        await PublishDomainEventsAsync(page.GetDomainEvents());

        return CommandResult.Create(entityId: request.FactId);
    }

    private async Task<string> SaveImageFileAsync(string pageId, string factId, string base64Image, string webRootPath)
    {
        var uploadsFolder = Path.Combine(webRootPath, "pages", pageId, "facts");

        Directory.CreateDirectory(uploadsFolder);

        var imageData = Convert.FromBase64String(base64Image);
        var fileName = $"{factId}.jpg";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await File.WriteAllBytesAsync(filePath, imageData);

        return $"/pages/{pageId}/facts/{fileName}";
    }
}

