using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.Services.Pages;

public interface IPageService
{
    /// <summary>
    /// Retrieves a page by its unique identifier.
    /// </summary>
    Task<Result<GetPageDto>> GetPage(string id);

    /// <summary>
    /// Creates a new page with the specified data.
    /// </summary>
    Task<Result<CommandResult>> CreatePage(CreatePageDto input);

    /// <summary>
    /// Updates an existing page with new data.
    /// </summary>
    Task<Result<CommandResult>> UpdatePage(string id, UpdatePageDto input);

    /// <summary>
    /// Searches for pages based on specified criteria.
    /// </summary>
    Task<Result<SearchOutputDto<GetPageDto>>> SearchPages(SearchPagesInputDto input);
}
