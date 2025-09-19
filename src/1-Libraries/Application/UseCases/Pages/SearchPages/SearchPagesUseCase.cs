using AutoMapper;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Contracts.Services;
using CodeBlock.DevKit.Core.Extensions;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HeyItIsMe.Application.UseCases.Pages.SearchPages;

/// <summary>
/// Use case for searching Page entities with advanced filtering and pagination.
/// This class demonstrates how to implement search query handlers that include:
/// - Complex search criteria with multiple filters
/// - Pagination and sorting
/// - User email to user ID conversion for enhanced search capabilities
/// - AutoMapper integration for DTO mapping
/// - Enriching results with additional user information
/// </summary>
internal class SearchPagesUseCase : BaseQueryHandler, IRequestHandler<SearchPagesRequest, SearchOutputDto<GetPageDto>>
{
    private readonly IPageRepository _pageRepository;
    private readonly IUserAccessorService _userAccessorService;

    /// <summary>
    /// Initializes a new instance of the SearchPagesUseCase with the required dependencies.
    /// </summary>
    /// <param name="pageRepository">The repository for page operations</param>
    /// <param name="mapper">The AutoMapper instance for object mapping</param>
    /// <param name="logger">The logger for this use case</param>
    /// <param name="userAccessorService">The service for accessing user information</param>
    public SearchPagesUseCase(
        IPageRepository pageRepository,
        IMapper mapper,
        ILogger<SearchPagesUseCase> logger,
        IUserAccessorService userAccessorService
    )
        : base(mapper, logger)
    {
        _pageRepository = pageRepository;
        _userAccessorService = userAccessorService;
    }

    /// <summary>
    /// Handles the search for pages based on various criteria.
    /// This method demonstrates a complete search flow including:
    /// 1. Converting user email search terms to user IDs for enhanced search
    /// 2. Executing the search with multiple filters and pagination
    /// 3. Mapping results to DTOs
    /// 4. Enriching DTOs with user email information
    /// </summary>
    /// <param name="request">The search request containing all search criteria</param>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>Search results with pagination information and enriched user data</returns>
    public async Task<SearchOutputDto<GetPageDto>> Handle(SearchPagesRequest request, CancellationToken cancellationToken)
    {
        // Replace the searched user email with user Id if the term is a valid email
        // This allows searching by user email as well as user Id
        var term = await ReplaceSearchedUserEmailWithUserId(request.Term);

        var pages = await _pageRepository.SearchAsync(
            term,
            null, // userId - not filtering by user in this search
            request.PageNumber,
            request.RecordsPerPage,
            request.SortOrder,
            request.FromDateTime,
            request.ToDateTime
        );

        var totalRecords = await _pageRepository.CountAsync(request.Term, null, request.FromDateTime, request.ToDateTime);

        var pagesDto = _mapper.Map<IEnumerable<GetPageDto>>(pages);

        // Fetch the email associated with the user Id
        foreach (var pageDto in pagesDto)
            pageDto.UserEmail = await _userAccessorService.GetEmailByUserIdIfExists(pageDto.UserId);

        return new SearchOutputDto<GetPageDto> { TotalRecords = totalRecords, Items = pagesDto };
    }

    /// <summary>
    /// Converts a user email search term to a user ID if the term is a valid email address.
    /// This method enhances search capabilities by allowing users to search by email
    /// while maintaining the underlying search by user ID.
    /// </summary>
    /// <param name="term">The search term that might be an email address</param>
    /// <returns>The original term or the converted user ID if the term was an email</returns>
    private async Task<string> ReplaceSearchedUserEmailWithUserId(string term)
    {
        if (term.IsNullOrEmptyOrWhiteSpace())
            return string.Empty;

        term = term.Trim();

        if (term.IsValidEmail())
        {
            var userId = await _userAccessorService.GetUserIdByEmailIfExists(term);
            if (!userId.IsNullOrEmptyOrWhiteSpace())
                term = userId;
        }

        return term;
    }
}
