using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.Services.Pages;

/// <summary>
/// Service interface for managing Page entities.
/// This interface demonstrates how to define service contracts with proper return types
/// and async operations following CQRS patterns.
/// </summary>
public interface IPageService
{
    /// <summary>
    /// Retrieves a page by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the page</param>
    /// <returns>A result containing the page data or an error</returns>
    Task<Result<GetPageDto>> GetPage(string id);

    /// <summary>
    /// Creates a new page with the specified data.
    /// </summary>
    /// <param name="input">The data for creating the new page</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> CreatePage(CreatePageDto input);

    /// <summary>
    /// Updates an existing page with new data.
    /// </summary>
    /// <param name="id">The unique identifier of the page to update</param>
    /// <param name="input">The new data for the page</param>
    /// <returns>A result containing the command execution result or an error</returns>
    Task<Result<CommandResult>> UpdatePage(string id, UpdatePageDto input);

    /// <summary>
    /// Searches for pages based on specified criteria.
    /// </summary>
    /// <param name="input">The search criteria and pagination parameters</param>
    /// <returns>A result containing the search results with pagination information</returns>
    Task<Result<SearchOutputDto<GetPageDto>>> SearchPages(SearchPagesInputDto input);
}
