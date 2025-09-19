using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Services;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.Exceptions;
using HeyItIsMe.Application.Helpers;
using HeyItIsMe.Core.Domain.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.GetPage;

/// <summary>
/// Use case for retrieving a Page entity by its identifier.
/// This class demonstrates how to implement query handlers that include:
/// - Permission-based access control
/// - User context validation
/// - AutoMapper integration for DTO mapping
/// - Proper error handling with custom exceptions
/// </summary>
internal class GetPageUseCase : BaseQueryHandler, IRequestHandler<GetPageRequest, GetPageDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserAccessorService _userAccessorService;

    /// <summary>
    /// Initializes a new instance of the GetPageUseCase with the required dependencies.
    /// </summary>
    /// <param name="pageRepository">The repository for page operations</param>
    /// <param name="mapper">The AutoMapper instance for object mapping</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="currentUser">The current user context</param>
    /// <param name="userAccessorService">The service for accessing user information</param>
    public GetPageUseCase(
        IPageRepository pageRepository,
        IMapper mapper,
        ILogger<GetPageUseCase> logger,
        ICurrentUser currentUser,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _currentUser = currentUser;
        _userAccessorService = userAccessorService;
    }

    /// <summary>
    /// Handles the retrieval of a page by its identifier.
    /// This method demonstrates a complete query flow including:
    /// 1. Entity retrieval from repository
    /// 2. Permission-based access control
    /// 3. DTO mapping with AutoMapper
    /// 4. Enriching DTO with additional user information
    /// </summary>
    /// <param name="request">The query request containing the page identifier</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>The page DTO with enriched user information</returns>
    /// <exception cref="PageApplicationExceptions">Thrown when the page is not found</exception>
    public async Task<GetPageDto> Handle(GetPageRequest request, CancellationToken cancellationToken)
    {
        var page = await _pageRepository.GetByIdAsync(request.Id);
        if (page == null)
            throw PageApplicationExceptions.PageNotFound(request.Id);

        // Ensures that the current user has permission to access the specified data
        EnsureUserHasAccess(page.UserId, _currentUser, Permissions.Page.PAGES);

        var pageDto = _mapper.Map<GetPageDto>(page);

        // Fetch the email associated with the user Id
        pageDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(page.UserId);

        return pageDto;
    }
}
