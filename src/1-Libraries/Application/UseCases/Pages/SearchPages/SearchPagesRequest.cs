using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Contracts.Dtos;
using CodeBlock.DevKit.Core.Helpers;
using HeyItIsMe.Application.Dtos.Pages;

namespace HeyItIsMe.Application.UseCases.Pages.SearchPages;

internal class SearchPagesRequest : BaseQuery<SearchOutputDto<GetPageDto>>
{
    public SearchPagesRequest(
        string term,
        int pageNumber,
        int recordsPerPage,
        SortOrder sortOrder,
        DateTime? fromDateTime,
        DateTime? toDateTime,
        QueryOptions options = null
    )
        : base(options)
    {
        Term = term;
        RecordsPerPage = recordsPerPage;
        PageNumber = pageNumber;
        SortOrder = sortOrder;
        FromDateTime = fromDateTime;
        ToDateTime = toDateTime;
    }

    /// <summary>
    /// The sort order for the search results.
    /// </summary>
    public SortOrder SortOrder { get; }

    /// <summary>
    /// The search term for filtering by route, display name, or user information.
    /// </summary>
    public string Term { get; }

    /// <summary>
    /// The number of records to return per page.
    /// </summary>
    public int RecordsPerPage { get; }

    /// <summary>
    /// The page number for pagination (1-based).
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Optional start date for filtering by creation date.
    /// </summary>
    public DateTime? FromDateTime { get; }

    /// <summary>
    /// Optional end date for filtering by creation date.
    /// </summary>
    public DateTime? ToDateTime { get; }
}
