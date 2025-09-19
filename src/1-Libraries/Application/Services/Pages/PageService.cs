using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;
using HeyItIsMe.Application.UseCases.Pages.CreatePage;
using HeyItIsMe.Application.UseCases.Pages.GetPage;
using HeyItIsMe.Application.UseCases.Pages.SearchPages;
using HeyItIsMe.Application.UseCases.Pages.UpdatePage;

namespace HeyItIsMe.Application.Services.Pages;

/// <summary>
/// Application service for managing Page entities.
/// This class demonstrates how to implement application services that coordinate between
/// different use cases and provide a clean API for the presentation layer.
/// </summary>
internal class PageService : ApplicationService, IPageService
{
    /// <summary>
    /// Initializes a new instance of the PageService with the required dependencies.
    /// </summary>
    /// <param name="requestDispatcher">The request dispatcher for sending commands and queries</param>
    public PageService(IRequestDispatcher requestDispatcher)
        : base(requestDispatcher) { }

    /// <summary>
    /// Retrieves a page by its unique identifier.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="id">The unique identifier of the page</param>
    /// <returns>A result containing the page data or an error</returns>
    public async Task<Result<GetPageDto>> GetPage(string id)
    {
        return await _requestDispatcher.SendQuery(new GetPageRequest(id));
    }

    /// <summary>
    /// Creates a new page with the specified data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="input">The data for creating the new page</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> CreatePage(CreatePageDto input)
    {
        return await _requestDispatcher.SendCommand(new CreatePageRequest(input.Route, input.DisplayName, input.UserId, input.SubscriptionId));
    }

    /// <summary>
    /// Updates an existing page with new data.
    /// This method demonstrates how to delegate to use cases using the request dispatcher.
    /// </summary>
    /// <param name="id">The unique identifier of the page to update</param>
    /// <param name="input">The new data for the page</param>
    /// <returns>A result containing the command execution result or an error</returns>
    public async Task<Result<CommandResult>> UpdatePage(string id, UpdatePageDto input)
    {
        return await _requestDispatcher.SendCommand(new UpdatePageRequest(id, input.Route, input.DisplayName));
    }

    /// <summary>
    /// Searches for pages based on specified criteria.
    /// This method demonstrates how to delegate to use cases using the request dispatcher
    /// and map complex input parameters.
    /// </summary>
    /// <param name="input">The search criteria and pagination parameters</param>
    /// <returns>A result containing the search results with pagination information</returns>
    public async Task<Result<SearchOutputDto<GetPageDto>>> SearchPages(SearchPagesInputDto input)
    {
        return await _requestDispatcher.SendQuery(
            new SearchPagesRequest(
                input.Term,
                input.PageNumber,
                input.RecordsPerPage,
                input.SortOrder,
                input.FromDateTime,
                input.ToDateTime
            )
        );
    }
}
